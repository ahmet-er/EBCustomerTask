# EBCustomerTask

**EBCustomerTask** bir m��teri y�netim sistemi uygulamas�d�r. Uygulama, kullan�c� kay�t ve giri� i�lemleri, m��teri y�netimini destekler.

## �zellikler

- **Kullan�c� Kay�t ve Giri�:** Kullan�c�lar�n sisteme kay�t olmas�n� ve giri� yapmas�n� sa�lar.
- **M��teri Y�netimi:** M��terileri ekleyip y�netir, m��teri bilgilerini g�r�nt�ler.

## Teknolojiler ve K�t�phaneler

- **ASP.NET Core 8:** Web uygulamas� geli�tirme ve yap�land�rma.
- **Entity Framework Core:** Veritaban� eri�imi ve y�netimi.
- **SQLServer:** �li�kisel veritaban�.
- **MongoDB:** NoSQL veritaban�.
- **DinkToPdf:** HTML'den PDF olu�turma.
- **OpenXML:** Excel tablosu olu�turma
- **ASP.NET Identity:** Kullan�c� kimlik do�rulama ve yetkilendirme.
- **AutoMapper:** Nesne d�n��t�rme ve e�leme.
- **FluentValidation:** Model do�rulama.
- **Serilog:** Loglama ve hata raporlama.
- **Bogus:** Data seed i�lemi i�in fake data olu�turma.

## Kurulum

1. Projeyi klonlay�n: `git clone <https://github.com/ahmet-er/EBCustomerTask.git>`
2. `appsettings.json` dan `ConnectionStrings` k�sm�nda `SqlServer` i�in kendi connection stringinizi girin. (Uygulama default olarak SQLServer kullan�r. Atlas MongoDb i�in benim database'i kullanacakt�r, isterseniz onuda de�i�tirebilirsiniz.)
3. Uygulamay� ba�lat�n: `dotnet run`(Veritaban� migration'lar� uygulama ba�lad���nda otomatik olarak uygulanaca�� i�in ilk ba�latma yava� olacakt�r.)

## Default Kullan�c� Bilgileri
- T�m kullan�c�lar i�in �ifre: `Password12*`
1. Admin rol�ne sahip emailler: `admin1@gmail.com`, `admin2@gmail.com`
2. User rol�ne sahip emailler: `user1@gmail.com`, `user2@gmail.com`

## �zet
- Temel olarak 4 katman olu�turdum;
	1. Application: business katman�
	2. Core: domain katman�
	3. Infrastructure: veri taban� etkile�im katman�
	4. WebUI: presentation katman�
- Microsoft identity kulland�m bunun servisini, repositorysini yazd�m.
- Strategy pattern ile Admin rol�ne sahip kullan�c� uygulaman Settings k�sm�ndan veritaban�n� run-time'da de�i�tirebilir ve bu t�m kullan�c�lara yans�r.
- Register ile olu�turulan kullan�c�lar User rol�nde olu�turulur, Admin sadece uygulama aya�a kalkarken olu�turulur.
- Customer i�in olu�turdu�um ICustomerService ve onu implemente eden CustomerService class'� CustomerController'da kullanmak i�in yazd�m. ICustomerRepository ve onu implemente eden CustomerRepositoryFromSqlServer ve CustomerRepositoryFromMongoDb class'lar� ise temel CRUD operasyonlar�n� ger�ekle�tirmek ve bu repo'lar� i� katman�nda kullanarak, infrastructre ve web ui katman�n� ay�rm�� oldum.
- Role, Configuration ve DatabaseType'� Core'da enum olarak tuttum.
- Configuration i�in SQLServer'da bir entity olu�turudum yine onun servislerini ve repositorylerini olu�turdum. Key-value �eklinde ayarlar� tutmas� i�in, DatabaseType enum'dan gelen int'i burada tutar. (Admin settings'den de�i�tirdi�inde buradaki value de�i�ir.)
- Loglama i�in Serilog kulland�m. (Log'lar WebUI'da genel dizinde Logs klas�r� alt�nda 7 g�nl�k tutulur)
- Entity'leri olu�turdum configurationlar�n� ayr� yerde yapt�m ve AppIdentityDbContext'de bu config�rasyonlar� belirttim.
- Model'ler i�in her i�lev i�in ayr� ViewModel olu�turdum ve onlar�n girdi alanlar� FluentValidation kullanarak validasyonlar�n� yapt�m.
- M��teri tablosunu listelendi�i yerde live searchbox yapmak istedim, bu customer servise query yollay�p, oradan d�nen customer'lar� listeler, burada search inputunu izlemek i�in javascript kulland�m. S�rekli servera istek yapmamas� ad�na 300 ms gecikme yapmak istedim.
- Bu searchbox ile filtreleme yap�lan m��teri listesini, �stte toolbar'da pdf ve excel g�rsellerini koydu�um butonlar'la arama sonucu e�le�en m��teri listesini indirilebilir yapmak istedim. Bu i�lem i�in ayr� ayr� PdfService ve ExcelService olu�turudum.
- ViewModel ve entity'leri Automapper kullanarak mapledim.
- DataSeed olu�turdum, 2 User, 2 Admin rol�ne sahip kullan�c�, SQLServer'da Customers tablosu bo�sa 10 tane fake Customer kayd� olu�turur.Ayn� �ekilde MongoDb'de Configuration'da ve MongoDb'nin reposunda verilen Database ad� EBCustomerTaskDb ve Collection ad� Customers olan koleksiyon'da kay�t yoksa orayada 10 fake kay�t olu�turur. Her ikisi i�in'de Bogus k�t�phanesini kulland�m. 
- Kullan�c� foto'lar�n� local'e kaydeden IPhotoService ve onu implement eden PhotoService servis'ini yazd�m bu Customer'da foto ile alakal� olan yerlerde bu servisi kullan�r. Foto'yu al�r, local'e (wwwroot/photos) kay�t eder, url'sini d�ner, db'de url tutulur. 
- �z y�z i�in bootstrap'ten faydaland�m.

### Yapmay� d���nd���m �zellikler
Bu �zellikleri to-do'ya ekledim ama s�re kalmad��� i�in yapamad�m. Yine de s�ylemek isterim:
- M��teri say�s� artt�k�a daha iyi bir sunum ve performans i�in Pagination.
- Bilgilendirme i�in Toastr.
- Gmail smtp servisi yaz�p, filtrelenen Customer listesini mail olarak g�nderme.
- M��teri foto'lar�n� Settings k�sm�na lokasyonu local veya Azure Blob Storage olarak 2 se�enek sunup Admin'in bunu de�i�tirebilmesi. (Configuration'da enum de�eri tutulacak, DatabaseType gibi)