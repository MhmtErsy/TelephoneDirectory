# Telefon Rehberi

## Kapsam
__Sistem iki farklı arayüze sahip olacaktır.__

__Herkese açık arayüz (bundan sonra PublicUI olarak adlandırılacaktır)__

__Sadece Admin tarafından girilebilen arayüz (bundan sonra AdminUI olarak adlandırılacaktır)__

__Çalışanın ad, soyad, telefon, departman ve yönetici bilgileri sistemde yer alacaktır.__

__PublicUI sadece sistemde kayıtlı çalışanların adlarını ve telefon numaralarını barındıran bir liste gösterecektir. Buradan bir çalışan seçilmesi durumunda çalışana ait detay bilgi gösterimi yapılacaktır.__

__AdminUI için gerek şifre değiştirilebilir olacaktır.__

__AdminUI arayüzünden yeni çalışan girişi yapılabilecektir.__

__Çalışanın ad, soyad ve telefon bilgisinin girilmesi zorunludur.__

__Departman bilgisi, veritabanından alınarak dropdownlist’ten seçtirilecektir.__

__Yönetici bilgisi, dropdownlist’ten seçtirilecektir. Bu dropdownlist sistemde mevcut bulunan çalışan kayıtları üzerinden doldurulacaktır.__

__Çalışan kayıtları AdminUI üzerinden düzenlenebilecek ve silinebilecektir. Kural olarak eğer ilgili çalışan başka bir çalışanın yöneticisi statüsünde bulunuyor ise silme işlemine izin verilmeyecektir.__

__Sistemde kayıtlı bulanan departmanlar yönetilebilir olacaktır. Ekleme, Düzenleme ve Silme işlemlerine izin verilecektir.__

__Kural olarak ilgili departmanın altında tanımlı çalışan varsa departman silinemeyecektir.__
