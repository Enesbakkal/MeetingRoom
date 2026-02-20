# Meeting Room API
Room & Reservation REST API (.NET 8).

## Gereksinimler
- **.NET 8 SDK** — [İndir](https://dotnet.microsoft.com/download/dotnet/8.0). Projeyi çalıştırmak için gereklidir.
- **LocalDB** — Varsayılan veritabanı için (Visual Studio ile birlikte gelir). Projede connection string olarak `(LocalDb)\MSSQLLocalDB` kullanılıyor; sizin makinede instance adı farklı olabilir, gerekirse `appsettings.json` veya User Secrets içindeki connection string’i kendi ortamınıza göre düzeltmeniz gerekir.
- **Git** — Repoyu clone etmek için (`git clone https://github.com/...`). Çoğu geliştirici makinede zaten vardır.
- **dotnet-ef** (isteğe bağlı) — Sadece migration’ı komut satırından elle çalıştıracak veya yeni migration ekleyecekseniz gerekir. Uygulama ilk çalıştırmada migration’ı otomatik uyguladığı için sadece projeyi çalıştıracaksanız kurmanız gerekmez. Kurulum: `dotnet tool install --global dotnet-ef`

## Notlar (kod inceleyenler için)
- **Rezervasyon Serisi ve Exception** modülü **CQRS örneklemek** için eklendi (MediatR ile Command/Query ayrımı, handler’lar, ValidationBehavior).
- Seri (**ReservationSeries**) sadece tekrarlayan rezervasyonun **tanımıdır** (metadata). Seri oluşturmak **Reservations** tablosuna otomatik kayıt eklemez; ileride genişletilebilir. Bu tasarım Domain ve `CreateReservationSeriesCommandHandler` içinde kısa açıklamalarla belirtilmiştir.

## Çalıştırma
- Proje kökünde: `dotnet run --project MeetingRoom.Api` (veya IDE ile F5).
- Tüm endpoint'leri anlamlı verilerle denemek için: **[API-TESTS.md](API-TESTS.md)** (Swagger/Postman için hazır istekler).
- Swagger: `https://localhost:7111/swagger` (veya çıktıdaki port).
- Varsayılan veritabanı: **LocalDB**. `appsettings.json` (veya User Secrets) içindeki `ConnectionStrings:DefaultConnection` kullanılır. Projede `(LocalDb)\MSSQLLocalDB` yazıyor; sizin makinede instance adı farklıysa (örn. `(LocalDb)\v11.0` veya SQL Express `.\SQLEXPRESS`) connection string'i kendi ortamınıza göre güncelleyin.
- Uygulama ilk açılışta migration'ları uygular; seed data (örnek odalar ve rezervasyonlar) otomatik eklenir.
- Kendi SQL Server kullanmak için: `MeetingRoom.Infrastructure/Extensions/ServiceCollectionExtensions.cs` içindeki yorum satırını açıp kendi connection string'inizi yazın (repoya göndermeyin).

## Migration'lar
- Projede tek migration var: **InitialCreate** (tüm tablolar + seed). Uygulama başlarken `Migrate()` ile otomatik uygulanır.
- Veritabanını elle güncellemek için (örn. clone sonrası):
  ```bash
  dotnet ef database update --project MeetingRoom.Infrastructure --startup-project MeetingRoom.Api
  ```
- Yeni migration eklemek için:
  ```bash
  dotnet ef migrations add <Adi> --project MeetingRoom.Infrastructure --startup-project MeetingRoom.Api
  ```
