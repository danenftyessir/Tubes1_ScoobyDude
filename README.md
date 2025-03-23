# Tugas Besar 1 IF2211 Strategi Algoritma

## Kelompok 38 - ScoobyDude
| NIM | Nama |
|-----|------|
| 13523136 | Danendra Shafi Athallah |
| 13523141 | Jovandra Otniel Pangalambok Siregar |
| 13523151 | Ardell Aghna Mahendra |

## Deskripsi Singkat
Pada tugas besar ini kami membuat 4 bot, yaitu 1 bot utama dan 3 bot alternatif untuk permainan Robocode Tank Royale dengan mengimplementasikan algoritma Greedy. Implementasi dilakukan dengan bahasa C# (.NET) dan setiap bot menggunakan strategi Greedy dengan heuristik yang berbeda-beda.

## Bot Kelompok Kami

### ScoobyGodBot (Bot Utama)
Bot utama kami mengimplementasikan pendekatan greedy berdasarkan damage maksimum yang disesuaikan dengan kondisi energi bot. Bot ini secara dinamis mengatur tingkat agresivitas berdasarkan level energi:
- **Energi Tinggi (>70)**: Bertindak agresif dengan strategi menyerang
- **Energi Menengah (>30)**: Mengambil pendekatan seimbang (balanced) 
- **Energi Rendah (<30)**: Beralih ke strategi defensif

Pengaturan daya tembak juga disesuaikan dengan jarak target, yang mencerminkan pendekatan greedy untuk memaksimalkan damage output dengan mempertimbangkan efisiensi energi. Heuristik yang digunakan adalah pemilihan power dan strategi pergerakan berdasarkan kondisi energi.

### ScoobyLuicy (Bot Alternatif 1)
Bot ini menerapkan strategi greedy berdasarkan target terdekat, dengan prioritas utama pada efisiensi penembakan dan target acquisition. Bot ini secara konsisten melacak posisi target terdekat dan berusaha melakukan penembakan dengan daya maksimal untuk menghasilkan damage tertinggi dalam jangka pendek. Pendekatan ini mengutamakan reaksi cepat terhadap target terdekat tanpa mempertimbangkan efisiensi energi jangka panjang, yang merupakan karakteristik khas dari algoritma greedy. Heuristik yang digunakan adalah pengejaran target berdasarkan jarak terdekat.

### ScoobyShadowBot (Bot Alternatif 2)
Bot ini mengadopsi strategi greedy berbasis evasion dan scanning, mengoptimalkan kelangsungan hidup melalui pola gerakan zigzag yang dirancang untuk menghindar dari tembakan musuh sekaligus memaksimalkan area pemindaian. Daya tembak bot ini bervariasi berdasarkan jarak target, dengan fokus pada survival dan kemampuan untuk mendeteksi musuh secara efektif. Pendekatan ini mencerminkan strategi greedy yang memprioritaskan informasi dan kelangsungan hidup sebagai dasar pengambilan keputusan. Heuristik yang digunakan adalah pola pergerakan zigzag untuk memaksimalkan scanning dan evasion.

### ScoobyRoma3000 (Bot Alternatif 3)
Bot ini mengimplementasikan solusi greedy yang sangat agresif, dengan pengejaran target dan penembakan full power untuk memaksimalkan damage output dalam waktu singkat. Bot ini memiliki sedikit pertimbangan defensif, dengan fokus utama pada serangan dan ramming untuk memaksimalkan damage. Strategi ini mencerminkan pendekatan greedy yang memilih keuntungan maksimal jangka pendek tanpa memperhatikan konsekuensi jangka panjang seperti efisiensi energi atau survivability. Heuristik yang digunakan adalah pengejaran agresif dan penembakan daya maksimum untuk memaksimalkan damage output.

## Struktur Program
```
Tubes1_ScoobyDude/
├── docs/
│   └── ScoobyDude.pdf
│
├── src/
│   ├── main-bot/ScoobyGodBot/
│   │   ├── ScoobyGodBot.cmd
│   │   ├── ScoobyGodBot.cs
│   │   ├── ScoobyGodBot.csproj
│   │   ├── ScoobyGodBot.json
│   │   └── ScoobyGodBot.sh
│   │
│   └── alternative-bots/
│       ├── ScoobyLuicy/
│       │   ├── ScoobyLuicy.cmd
│       │   ├── ScoobyLuicy.cs
│       │   ├── ScoobyLuicy.csproj
│       │   ├── ScoobyLuicy.json
│       │   └── ScoobyLuicy.sh
│       │
│       ├── ScoobyShadowBot/
│       │   ├── ScoobyShadowBot.cmd
│       │   ├── ScoobyShadowBot.cs
│       │   ├── ScoobyShadowBot.csproj
│       │   ├── ScoobyShadowBot.json
│       │   └── ScoobyShadowBot.sh
│       │
│       └── ScoobyRoma3000/
│           ├── ScoobyRoma3000.cmd
│           ├── ScoobyRoma3000.cs
│           ├── ScoobyRoma3000.csproj
│           ├── ScoobyRoma3000.json
│           └── ScoobyRoma3000.sh
│
│
└── README.md
```

## Requirement Program dan Instalasi

### Persyaratan Sistem
- .NET SDK (minimal versi 6.0)
- Java Runtime Environment (untuk menjalankan game engine)

### Instalasi
1. Clone repository ini
   ```
   git clone https://github.com/danenftyessir/Tubes1_ScoobyDude.git
   ```
2. Download game engine yang dimodifikasi asisten dari [tubes1-if2211-starter-pack](https://github.com/Ariel-HS/tubes1-if2211-starter-pack/releases/tag/v1.0)

## Cara Menjalankan Permainan Robocode
1. Buka terminal/command prompt dan jalankan command berikut:
   ```
   java -jar robocode-tankroyale-gui-0.30.0.jar
   ```
2. Klik tombol "Config" di GUI
3. Klik "Bot Root Directories" dan tambahkan direktori yang berisi folder bot-bot yang ingin dijalankan
4. Klik tombol "Battle" di GUI
5. Selanjutnya klik "Start Battle"
6. Pilih bot yang ingin dipertandingkan dari bagian "Bot Directories"
7. Klik tombol "Boot →"
8. Apabila bot tidak berhasil diboot, silahkan masuk ke terminal folder bot anda dan jalankan perintah berikut:
   ```
   dotnet restore
   dotnet build
   ```
   Setelah itu balik lagi ke robocode dan lakukan kembali langkah 2-7
9. Setelah bot muncul di panel "Booted Bots" dan "Joined Bots", pilih bot dan klik "Add →"
10. Klik "Start Battle" untuk memulai pertandingan
11. Selamat bermain

## Author
- Danendra Shafi Athallah (13523136)
  Link Repository: https://github.com/danenftyessir
- Jovandra Otniel Pangalambok Siregar (13523141)
  Link Repository: https://github.com/jovan196
- Ardell Aghna Mahendra (13523151)
  Link Repository: https://github.com/ArdellAghna

## Bonus
Link Video Youtube: https://youtu.be/oZV8lH66iSo
