# \# eCommerceAppSolution

# 

# \## 📌 Deskripsi

# \*\*eCommerceAppSolution\*\* adalah aplikasi backend berbasis \*\*.NET 8\*\* yang dikembangkan sebagai bagian dari challenge internship.  

# Aplikasi ini mendemonstrasikan implementasi \*\*CRUD\*\*, \*\*autentikasi JWT\*\*, \*\*testing\*\*, serta \*\*dokumentasi API\*\* menggunakan Swagger.  

# 

# Struktur project menerapkan \*\*Layered Architecture\*\* untuk menjaga kode tetap modular, scalable, dan mudah diuji.

# 

# ---

# 

# \## 🏗️ Arsitektur Project

# Project ini dibagi menjadi \*\*4 layer utama\*\*:

# 

# 1\. \*\*Domain\*\*  

# &nbsp;  - Berisi entity dan interface dasar.  

# &nbsp;  - Tujuannya agar logika bisnis inti tidak bergantung pada implementasi teknis.  

# 

# 2\. \*\*Application\*\*  

# &nbsp;  - Berisi service, DTO (Data Transfer Object), validasi, serta mapping.  

# &nbsp;  - Menjembatani antara domain dan infrastruktur, memastikan aturan bisnis tetap konsisten.  

# 

# 3\. \*\*Infrastructure\*\*  

# &nbsp;  - Berisi konfigurasi database \*\*SQL Server\*\*, implementasi repository, migrations, dan middleware.  

# &nbsp;  - Menggunakan \*\*Entity Framework Core\*\* sebagai ORM untuk akses data.  

# 

# 4\. \*\*Host\*\*  

# &nbsp;  - Entry point aplikasi.  

# &nbsp;  - Berisi controller, konfigurasi dependency injection, dan setup pipeline (middleware, JWT, Swagger).  

# 

# ---

# 

# \## 🎯 Alasan Menggunakan Pattern Ini

# Pattern \*\*Layered Architecture\*\* dipilih dengan alasan:

# \- \*\*Separation of Concerns\*\* → setiap layer punya tanggung jawab jelas, sehingga kode lebih terorganisir.  

# \- \*\*Testability\*\* → business logic dapat diuji secara unit test tanpa tergantung database atau external service.  

# \- \*\*Scalability\*\* → memudahkan pengembangan fitur baru tanpa merusak struktur yang ada.  

# \- \*\*Maintainability\*\* → bug lebih cepat diisolasi karena kode dipisahkan berdasarkan lapisan tanggung jawab.  

# 

# Dengan pendekatan ini, aplikasi menjadi \*\*lebih fleksibel\*\*, \*\*rapi\*\*, dan siap untuk pengembangan jangka panjang.

# 

# ---

# 

# \## ⚙️ Teknologi yang Digunakan

# \- \*\*.NET 8\*\*  

# \- \*\*Entity Framework Core\*\*  

# \- \*\*Microsoft SQL Server\*\*  

# \- \*\*JWT (JSON Web Token) Authentication\*\*  

# \- \*\*Swagger / OpenAPI\*\* untuk dokumentasi API  

# \- \*\*xUnit\*\* untuk testing  

# 

# ---

# 

# \## 🚀 Cara Menjalankan Project

# 

# 1\. \*\*Clone repository\*\*

# &nbsp;  ```bash

# &nbsp;  git clone https://github.com/satriarizka/eCommerceAppSolution.git

# &nbsp;  cd eCommerceAppSolution



