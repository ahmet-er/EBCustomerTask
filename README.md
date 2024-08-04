# EBCustomerTask

**EBCustomerTask** bir müþteri yönetim sistemi uygulamasýdýr. Uygulama, kullanýcý kayýt ve giriþ iþlemleri, müþteri yönetimini destekler.

## Özellikler

- **Kullanýcý Kayýt ve Giriþ:** Kullanýcýlarýn sisteme kayýt olmasýný ve giriþ yapmasýný saðlar.
- **Müþteri Yönetimi:** Müþterileri ekleyip yönetir, müþteri bilgilerini görüntüler.

## Teknolojiler ve Kütüphaneler

- **ASP.NET Core 8:** Web uygulamasý geliþtirme ve yapýlandýrma.
- **Entity Framework Core:** Veritabaný eriþimi ve yönetimi.
- **SQLServer:** Ýliþkisel veritabaný.
- **MongoDB:** NoSQL veritabaný.
- **DinkToPdf:** HTML'den PDF oluþturma.
- **OpenXML:** Excel tablosu oluþturma
- **ASP.NET Identity:** Kullanýcý kimlik doðrulama ve yetkilendirme.
- **AutoMapper:** Nesne dönüþtürme ve eþleme.
- **FluentValidation:** Model doðrulama.
- **Serilog:** Loglama ve hata raporlama.
- **Bogus:** Data seed iþlemi için fake data oluþturma.

## Kurulum

1. Projeyi klonlayýn: `git clone <https://github.com/ahmet-er/EBCustomerTask.git>`
2. `appsettings.json` dan `ConnectionStrings` kýsmýnda `SqlServer` için kendi connection stringinizi girin. (Uygulama default olarak SQLServer kullanýr. Atlas MongoDb için benim database'i kullanacaktýr, isterseniz onuda deðiþtirebilirsiniz.)
3. Uygulamayý baþlatýn: `dotnet run`(Veritabaný migration'larý uygulama baþladýðýnda otomatik olarak uygulanacaðý için ilk baþlatma yavaþ olacaktýr.)

## Default Kullanýcý Bilgileri
- Tüm kullanýcýlar için þifre: `Password12*`
1. Admin rolüne sahip emailler: `admin1@gmail.com`, `admin2@gmail.com`
2. User rolüne sahip emailler: `user1@gmail.com`, `user2@gmail.com`

## Özet
- Temel olarak 4 katman oluþturdum;
	1. Application: business katmaný
	2. Core: domain katmaný
	3. Infrastructure: veri tabaný etkileþim katmaný
	4. WebUI: presentation katmaný
- Microsoft identity kullandým bunun servisini, repositorysini yazdým.
- Strategy pattern ile Admin rolüne sahip kullanýcý uygulaman Settings kýsmýndan veritabanýný run-time'da deðiþtirebilir ve bu tüm kullanýcýlara yansýr.
- Register ile oluþturulan kullanýcýlar User rolünde oluþturulur, Admin sadece uygulama ayaða kalkarken oluþturulur.
- Customer için oluþturduðum ICustomerService ve onu implemente eden CustomerService class'ý CustomerController'da kullanmak için yazdým. ICustomerRepository ve onu implemente eden CustomerRepositoryFromSqlServer ve CustomerRepositoryFromMongoDb class'larý ise temel CRUD operasyonlarýný gerçekleþtirmek ve bu repo'larý iþ katmanýnda kullanarak, infrastructre ve web ui katmanýný ayýrmýþ oldum.
- Role, Configuration ve DatabaseType'ý Core'da enum olarak tuttum.
- Configuration için SQLServer'da bir entity oluþturudum yine onun servislerini ve repositorylerini oluþturdum. Key-value þeklinde ayarlarý tutmasý için, DatabaseType enum'dan gelen int'i burada tutar. (Admin settings'den deðiþtirdiðinde buradaki value deðiþir.)
- Loglama için Serilog kullandým. (Log'lar WebUI'da genel dizinde Logs klasörü altýnda 7 günlük tutulur)
- Entity'leri oluþturdum configurationlarýný ayrý yerde yaptým ve AppIdentityDbContext'de bu configürasyonlarý belirttim.
- Model'ler için her iþlev için ayrý ViewModel oluþturdum ve onlarýn girdi alanlarý FluentValidation kullanarak validasyonlarýný yaptým.
- Müþteri tablosunu listelendiði yerde live searchbox yapmak istedim, bu customer servise query yollayýp, oradan dönen customer'larý listeler, burada search inputunu izlemek için javascript kullandým. Sürekli servera istek yapmamasý adýna 300 ms gecikme yapmak istedim.
- Bu searchbox ile filtreleme yapýlan müþteri listesini, üstte toolbar'da pdf ve excel görsellerini koyduðum butonlar'la arama sonucu eþleþen müþteri listesini indirilebilir yapmak istedim. Bu iþlem için ayrý ayrý PdfService ve ExcelService oluþturudum.
- ViewModel ve entity'leri Automapper kullanarak mapledim.
- DataSeed oluþturdum, 2 User, 2 Admin rolüne sahip kullanýcý, SQLServer'da Customers tablosu boþsa 10 tane fake Customer kaydý oluþturur.Ayný þekilde MongoDb'de Configuration'da ve MongoDb'nin reposunda verilen Database adý EBCustomerTaskDb ve Collection adý Customers olan koleksiyon'da kayýt yoksa orayada 10 fake kayýt oluþturur. Her ikisi için'de Bogus kütüphanesini kullandým. 
- Kullanýcý foto'larýný local'e kaydeden IPhotoService ve onu implement eden PhotoService servis'ini yazdým bu Customer'da foto ile alakalý olan yerlerde bu servisi kullanýr. Foto'yu alýr, local'e (wwwroot/photos) kayýt eder, url'sini döner, db'de url tutulur. 
- Öz yüz için bootstrap'ten faydalandým.

### Yapmayý düþündüðüm özellikler
Bu özellikleri to-do'ya ekledim ama süre kalmadýðý için yapamadým. Yine de söylemek isterim:
- Müþteri sayýsý arttýkça daha iyi bir sunum ve performans için Pagination.
- Bilgilendirme için Toastr.
- Gmail smtp servisi yazýp, filtrelenen Customer listesini mail olarak gönderme.
- Müþteri foto'larýný Settings kýsmýna lokasyonu local veya Azure Blob Storage olarak 2 seçenek sunup Admin'in bunu deðiþtirebilmesi. (Configuration'da enum deðeri tutulacak, DatabaseType gibi)