# 🅿️ OtoparkApp — Otopark Yönetim Sistemi

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-Razor_Pages-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-Express-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-8.0.8-68217A?style=for-the-badge" />
  <img src="https://img.shields.io/badge/lisans-MIT-green?style=for-the-badge" />
</p>

> **OtoparkApp**, otopark işletmeleri için geliştirilmiş web tabanlı bir yönetim uygulamasıdır.  
> Araç kayıtları, giriş/çıkış takibi ve araç tipine göre kademeli ücret hesaplama işlemlerini tek bir arayüzden kolayca yönetmenizi sağlar.

---

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Teknoloji Stack](#-teknoloji-stack)
- [Proje Yapısı](#-proje-yapısı)
- [Veri Modeli](#-veri-modeli)
- [Tarife Hesaplama Mantığı](#-tarife-hesaplama-mantığı)
- [Kurulum](#-kurulum)
- [Kullanım](#-kullanım)
- [Katkıda Bulunma](#-katkıda-bulunma)

---

## ✨ Özellikler

### 🚗 Araç Yönetimi (CRUD)
- Araçları **plaka**, **araç tipi**, **sahip adı** ve **sahip telefonu** bilgileriyle kayıt altına alma
- Araç listesini görüntüleme, düzenleme ve silme
- Araç bazında giriş/çıkış kayıtlarına navigation property desteği

### 🔐 Araç Giriş / Çıkış Takibi
- Seçili araç için anlık giriş kaydı oluşturma
- Çıkış işleminde otomatik **süre** ve **ücret** hesaplama
- Ödeme durumu yönetimi (`Bekliyor` / `Ödendi`)
- Aktif ve tamamlanan kayıtların listelenmesi

### 💰 Kademeli Tarife Sistemi (CRUD)
- Araç tipine göre özelleştirilebilir fiyat tanımları
- **3 kademeli** dinamik fiyatlandırma yapısı:
  - **0–1 saat** → Sabit ücret (`IlkSaatUcret`)
  - **1–3 saat** → Saatlik oran (`OrtaSaatUcret`)
  - **3+ saat** → İndirimli saatlik oran (`UzunSaatUcret`)

### 📦 DTO Katmanı
Tüm entity modelleri için ayrı **Data Transfer Object (DTO)** sınıfları mevcut; UI katmanı doğrudan veritabanı entity'leriyle değil, DTO'larla çalışır.

---

## 🛠️ Teknoloji Stack

| Katman | Teknoloji |
|---|---|
| Web Framework | ASP.NET Core 8.0 — Razor Pages |
| ORM | Entity Framework Core 8.0.8 |
| Veritabanı | Microsoft SQL Server (Express) |
| Veri Transferi | DTO (Data Transfer Object) Deseni |
| Dil | C# / .NET 8 |

---

## 📁 Proje Yapısı

```
OtoparkApp/
├── Models/                         # Veri modelleri
│   ├── Arac.cs                     # Araç entity'si
│   ├── AracDto.cs                  # Araç DTO'su
│   ├── GirisKayit.cs               # Giriş/çıkış kayıt entity'si
│   ├── GirisKayitDto.cs            # Kayıt DTO'su
│   ├── Tarife.cs                   # Tarife entity'si
│   └── TarifeDto.cs                # Tarife DTO'su
├── Pages/                          # Razor Pages (UI katmanı)
│   ├── Index.cshtml                # Ana sayfa
│   ├── Araclar/                    # Araç yönetimi
│   │   ├── Index.cshtml            # Araç listesi
│   │   ├── Create.cshtml           # Yeni araç ekleme
│   │   ├── Edit.cshtml             # Araç düzenleme
│   │   └── Delete.cshtml           # Araç silme
│   ├── GirisKayitlari/             # Otopark işlemleri
│   │   ├── Index.cshtml            # Kayıt listesi
│   │   ├── Create.cshtml           # Araç giriş formu
│   │   └── Cikis.cshtml            # Çıkış & ücret hesaplama
│   ├── Tarife/                     # Tarife yönetimi
│   │   ├── Index.cshtml            # Tarife listesi
│   │   ├── Create.cshtml           # Yeni tarife ekleme
│   │   ├── Edit.cshtml             # Tarife düzenleme
│   │   └── Delete.cshtml           # Tarife silme
│   └── Shared/                     # Ortak layout ve bileşenler
├── Services/
│   └── ApplicationDbContext.cs     # EF Core veritabanı bağlamı
├── Migrations/                     # EF Core migration dosyaları
├── Program.cs                      # Uygulama başlangıç noktası
├── appsettings.json                # Bağlantı ve yapılandırma
└── OtoparkApp.csproj               # Proje dosyası (.NET 8)
```

---

## 🗃️ Veri Modeli

```
┌──────────────────────┐       ┌─────────────────────────┐
│         Arac         │──────▶│       GirisKayit         │
│──────────────────────│  1:N  │─────────────────────────│
│ Id (PK)              │       │ Id (PK)                  │
│ Plaka                │       │ AracId (FK)              │
│ AracTipi             │       │ GirisTarihi              │
│ SahibiAdi (?)        │       │ CikisTarihi (?)          │
│ SahibiTelefon (?)    │       │ ToplamSureDakika (?)     │
│ OlusturmaTarihi      │       │ OdenenUcret (?)          │
└──────────────────────┘       │ OdemeDurumu              │
                               │   → "Bekliyor" / "Ödendi"│
                               └─────────────────────────┘

┌──────────────────────────┐
│          Tarife          │
│──────────────────────────│
│ Id (PK)                  │
│ AracTipi                 │  ← "Otomobil", "Motosiklet" vb.
│ IlkSaatUcret  (decimal)  │  ← 0-1 saat → sabit ücret
│ OrtaSaatUcret (decimal)  │  ← 1-3 saat → saatlik oran
│ UzunSaatUcret (decimal)  │  ← 3+ saat → saatlik oran
└──────────────────────────┘
```

> `(?)` işaretli alanlar nullable'dır; araç çıkış yapılmadan önce `null` değer taşır.

---

## 💡 Tarife Hesaplama Mantığı

Ücret hesaplaması, `Pages/GirisKayitlari/Cikis.cshtml.cs` içindeki `HesaplaUcret` metodu tarafından otomatik yapılır.

**T** = Toplam park süresi (saat cinsinden, dakikadan dönüştürülmüş)

```
T ≤ 1 saat
    Ücret = IlkSaatUcret

1 < T ≤ 3 saat
    Ücret = IlkSaatUcret + (T - 1) × OrtaSaatUcret

T > 3 saat
    Ücret = IlkSaatUcret + (2 × OrtaSaatUcret) + (T - 3) × UzunSaatUcret

Tarife bulunamazsa → Ücret = 0
```

### Örnek Hesaplama

| Araç Tipi | IlkSaatUcret | OrtaSaatUcret | UzunSaatUcret |
|---|---|---|---|
| Otomobil | ₺20 | ₺15/sa | ₺10/sa |

| Senaryo | Süre | Hesaplama | Toplam |
|---|---|---|---|
| Kısa park | 45 dk | ₺20 | **₺20** |
| Orta park | 2 saat | ₺20 + (1 × ₺15) | **₺35** |
| Uzun park | 5 saat | ₺20 + (2 × ₺15) + (2 × ₺10) | **₺70** |

---

## 🚀 Kurulum

### Ön Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/tr-tr/sql-server/sql-server-downloads) veya tam sürüm SQL Server
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ya da [VS Code](https://code.visualstudio.com/)

### 1. Repoyu Klonlayın

```bash
git clone https://github.com/Emircn10/OtoparkApp.git
cd OtoparkApp
```

### 2. Veritabanı Bağlantısını Yapılandırın

`appsettings.json` dosyasını kendi SQL Server örneğinize göre düzenleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=OtoparkDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

> **İpucu:** SQL Server Express yerine farklı bir instance kullanıyorsanız `Server` değerini değiştirin.  
> Örn: `Server=localhost` veya `Server=(localdb)\\mssqllocaldb`

### 3. Migration'ları Uygulayın

```bash
dotnet ef database update
```

> Migration dosyaları repoda hazır mevcuttur; bu komut doğrudan veritabanı tablolarını oluşturur.

### 4. Uygulamayı Çalıştırın

```bash
dotnet run
```

Ardından tarayıcınızda şu adresi açın: `https://localhost:5001`

---

## 🖥️ Kullanım

### Temel İş Akışı

```
1. Tarife Tanımla
   → /Tarife/Create → Araç tipine göre fiyat kademeleri ekle
          ↓
2. Araç Kaydet
   → /Araclar/Create → Plaka, tip ve sahip bilgilerini gir
          ↓
3. Araç Girişi Oluştur
   → /GirisKayitlari/Create → Araç seç → Kaydet
          ↓
4. Araç Çıkışı ve Ödeme
   → /GirisKayitlari → Aktif kayıt üzerinde "Çıkış" butonuna tıkla
   → Sistem süreyi ve ücreti otomatik hesaplar
          ↓
5. Geçmişi İncele
   → /GirisKayitlari → Tüm kayıtları filtrele ve görüntüle
```

### Sayfa Rehberi

| Sayfa | URL | Açıklama |
|---|---|---|
| Ana Sayfa | `/` | Uygulama karşılama sayfası |
| Araç Listesi | `/Araclar` | Tüm kayıtlı araçlar |
| Yeni Araç Ekle | `/Araclar/Create` | Araç kayıt formu |
| Araç Düzenle | `/Araclar/Edit/{id}` | Araç bilgilerini güncelle |
| Kayıt Listesi | `/GirisKayitlari` | Aktif ve geçmiş park kayıtları |
| Giriş Kaydı | `/GirisKayitlari/Create` | Araç giriş formu |
| Çıkış İşlemi | `/GirisKayitlari/Cikis` | Çıkış ve otomatik ücret hesaplama |
| Tarife Listesi | `/Tarife` | Tanımlı tarife listesi |
| Yeni Tarife | `/Tarife/Create` | Tarife oluşturma formu |

---

## 🤝 Katkıda Bulunma

1. Bu repoyu **fork**'layın
2. Yeni bir feature branch oluşturun:
   ```bash
   git checkout -b feature/yeni-ozellik
   ```
3. Değişikliklerinizi commit edin:
   ```bash
   git commit -m "feat: yeni özellik eklendi"
   ```
4. Branch'inizi push edin:
   ```bash
   git push origin feature/yeni-ozellik
   ```
5. **Pull Request** açın

---


---

<p align="center">
  <b>Emircn10</b> tarafından geliştirilmiştir &nbsp;•&nbsp;
  <a href="https://github.com/Emircn10/OtoparkApp">GitHub'da Görüntüle</a>
</p>
