# API Test Rehberi

Uygulama çalışırken (`dotnet run --project MeetingRoom.Api`) aşağıdaki istekleri Swagger veya Postman/curl ile deneyebilirsiniz. Base URL: `https://localhost:7111` (port çıktıda farklıysa onu kullanın).

---

## 1. Rooms (Odalar)

### 1.1 Tüm odaları listele
- **GET** `https://localhost:7111/api/Rooms`
- Body: yok
- Beklenen: 200, seed’den 3 oda (Toplantı Odası A, B, Konferans Salonu)

### 1.2 Tek oda getir
- **GET** `https://localhost:7111/api/Rooms/{id}` — path:
  - `id: 1`
- Beklenen: 200, Id=1 odanın detayı

### 1.3 Olmayan oda (404)
- **GET** `https://localhost:7111/api/Rooms/{id}` — path:
  - `id: 99`
- Beklenen: 404

### 1.4 Oda oluştur
- **POST** `https://localhost:7111/api/Rooms`
- Body (JSON):
```json
{
  "name": "Küçük Toplantı Odası",
  "capacity": 4,
  "floor": 1,
  "equipment": "TV"
}
```
- Beklenen: 201, oluşan oda (Id dönüyor)

### 1.5 Oda güncelle
- **PUT** `https://localhost:7111/api/Rooms/{id}` — path:
  - `id: 4` (1.4’te oluşan Id)
- Body (JSON):
```json
{
  "name": "Küçük Toplantı Odası (Güncel)",
  "capacity": 6,
  "floor": 1,
  "equipment": "TV, Projeksiyon"
}
```
- Beklenen: 200, güncellenmiş oda

### 1.6 Validation hatası (oda)
- **POST** `https://localhost:7111/api/Rooms`
- Body (JSON):
```json
{
  "name": "",
  "capacity": -1,
  "floor": 0
}
```
- Beklenen: 400, validation mesajları

---

## 2. Reservations (Rezervasyonlar)

### 2.1 Tüm rezervasyonları listele
- **GET** `https://localhost:7111/api/Reservations`
- Beklenen: 200, seed’den 3 rezervasyon

### 2.2 Filtreli listele (oda, kullanıcı, tarih aralığı)
- **GET** `https://localhost:7111/api/Reservations` — query (birini kullanın):
  - `roomId=1`
  - `userName=ali@firma.com`
  - `from=2025-02-01`
  - `to=2025-02-28`
- Beklenen: 200, filtrelenmiş liste

### 2.3 Tek rezervasyon getir
- **GET** `https://localhost:7111/api/Reservations/{id}` — path:
  - `id: 1`
- Beklenen: 200, rezervasyon detayı

### 2.4 Çakışma kontrolü
- **GET** `https://localhost:7111/api/Reservations/conflicts` — query:
  - `roomId=1`
  - `start=2025-02-02T09:00:00`
  - `end=2025-02-02T10:00:00`
- Beklenen: 200, çakışan rezervasyonlar (seed’de 1 numaralı rezervasyon bu aralıkta)

**`excludeReservationId` (opsiyonel):** Rezervasyon **güncellerken** aynı slot için çakışma sorulduğunda, güncellediğiniz rezervasyonun kendisi de “çakışan” listede çıkar. Bu parametreyle o Id’yi listeden hariç tutarsınız; böylece “bu slotta benden başka çakışan var mı?” sorusunu doğru yanıtlarsınız. Yeni rezervasyon oluştururken kullanmazsınız; sadece PUT (güncelle) öncesi çakışma kontrolünde kullanın.

### 2.4a Çakışma — excludeReservationId olmadan
- **GET** `https://localhost:7111/api/Reservations/conflicts` — query:
  - `roomId=1`
  - `start=2025-02-02T09:00:00`
  - `end=2025-02-02T10:00:00`
- Beklenen: 200, `data` içinde 1 eleman (Id=1 rezervasyon; o slotta zaten o var)

### 2.4b Çakışma — excludeReservationId ile (rezervasyon 1’i güncelliyormuş gibi)
- **GET** `https://localhost:7111/api/Reservations/conflicts` — query:
  - `roomId=1`
  - `start=2025-02-02T09:00:00`
  - `end=2025-02-02T10:00:00`
  - `excludeReservationId=1`
- Beklenen: 200, `data` boş liste (Id=1 hariç tutulduğu için “benden başka çakışan yok”)

### 2.5 Rezervasyon oluştur
- **POST** `https://localhost:7111/api/Reservations`
- Body (JSON) — çakışmayan bir aralık seçin:
```json
{
  "roomId": 2,
  "userName": "fatma@firma.com",
  "startTime": "2025-03-01T10:00:00",
  "endTime": "2025-03-01T11:00:00"
}
```
- Beklenen: 201, oluşan rezervasyon

### 2.6 Rezervasyon güncelle
- **PUT** `https://localhost:7111/api/Reservations/{id}` — path:
  - `id: 4` (2.5’te oluşan Id)
- Body (JSON):
```json
{
  "startTime": "2025-03-01T14:00:00",
  "endTime": "2025-03-01T15:00:00"
}
```
- Beklenen: 200, güncellenmiş rezervasyon

### 2.7 Rezervasyon iptal (soft delete)
- **DELETE** `https://localhost:7111/api/Reservations/{id}` — path:
  - `id: 4`
- Beklenen: 200, “Reservation canceled.”

### 2.8 Validation hatası (rezervasyon)
- **POST** `https://localhost:7111/api/Reservations`
- Body (JSON):
```json
{
  "roomId": 1,
  "userName": "",
  "startTime": "2025-03-01T11:00:00",
  "endTime": "2025-03-01T10:00:00"
}
```
- Beklenen: 400 (EndTime < StartTime vb.)

---

## 3. Reservation Series (Rezervasyon Serisi – CQRS)

### 3.1 Tüm serileri listele
- **GET** `https://localhost:7111/api/ReservationSeries`
- Beklenen: 200, seed’den 2 seri (Haftalık Toplantı, Aylık Değerlendirme)

### 3.2 Tek seri getir
- **GET** `https://localhost:7111/api/ReservationSeries/{id}` — path:
  - `id: 1`
- Beklenen: 200, seri detayı

### 3.3 Seri oluştur
- **POST** `https://localhost:7111/api/ReservationSeries`
- Body (JSON):
```json
{
  "name": "Günlük Sync",
  "pattern": "Daily",
  "startDate": "2025-03-01T00:00:00",
  "endDate": "2025-03-31T00:00:00"
}
```
- Beklenen: 201, oluşan seri (Id dönüyor)

### 3.4 Seri exceptions listele
- **GET** `https://localhost:7111/api/ReservationSeries/{id}/exceptions` — path:
  - `id: 1` (seri Id)
- Beklenen: 200, seri 1’e ait exception’lar (seed’den 2 adet)

### 3.5 Seriye exception ekle
- **POST** `https://localhost:7111/api/ReservationSeries/{id}/exceptions` — path:
  - `id: 1` (seri Id)
- Body (JSON):
```json
{
  "exceptionDate": "2025-02-22T00:00:00"
}
```
- Beklenen: 201, oluşan exception kaydı

### 3.6 Olmayan seri (404)
- **GET** `https://localhost:7111/api/ReservationSeries/{id}` — path:
  - `id: 99`
- Beklenen: 404

---

## Kısa test sırası (copy-paste için)

1. **GET** `/api/Rooms` → listele  
2. **GET** `/api/Rooms/1` → tek oda  
3. **POST** `/api/Rooms` → yeni oda (body yukarıdaki gibi)  
4. **PUT** `/api/Rooms/4` → güncelle  
5. **GET** `/api/Reservations` → listele  
6. **GET** `/api/Reservations/conflicts?roomId=1&start=2025-02-02T09:00:00&end=2025-02-02T10:00:00` → çakışma (1 sonuç); aynı URL’e `&excludeReservationId=1` ekleyerek tekrar çağır → çakışma boş (güncelleme senaryosu)  
7. **POST** `/api/Reservations` → yeni rezervasyon  
8. **PUT** `/api/Reservations/4` → güncelle  
9. **DELETE** `/api/Reservations/4` → iptal  
10. **GET** `/api/ReservationSeries` → seri listele  
11. **GET** `/api/ReservationSeries/1` → tek seri  
12. **POST** `/api/ReservationSeries` → yeni seri  
13. **GET** `/api/ReservationSeries/1/exceptions` → exception listele  
14. **POST** `/api/ReservationSeries/1/exceptions` → exception ekle  

Tarihleri kendi ortamınıza göre (ör. bugünün tarihi) güncelleyebilirsiniz; seed 2025-02-01 civarı kullanıyor.
