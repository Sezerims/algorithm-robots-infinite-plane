# Sonsuz Düzlemdeki Robotlar

İki robot da inip paraşütlerini bıraktıktan sonra **aynı yönde** ilerlemeye başlıyor.

**Her ilerlemelerinde:** <br>
**1 birim ilerisini** diğer robotun **paraşütü** için, <br>
**1 birim çapını** diğer **robot** için kontrol ediyorlar.

---
Bir robot, diğerinin paraşütünü bulana kadar her seferinde **1 birim** ilerliyor. <br>
Paraşütü bulan robotun, diğer robotun gerisinde olduğu anlaşılıyor. <br>
Bu robot, paraşütü kontrol etmeyi bırakıyor. <br>
Diğer robota yetişebilmek için, her seferinde **2 birim** ilerlemeye başlıyor.

---
Bu şekilde, geride kalan robot, diğerine yetişiyor ve **birbirlerini gördüklerinde duruyorlar.**

---
Bu algoritmanın testi için multithreading kullandım çünkü robotların bu işlemleri paralel
olarak gerçekleştirebilmesi gerekiyor ancak bu yöntem her seferinde aynı sonuçları vermiyor.

Daha tutarlı sonuçlar için asenkron programlama kullanılabilir.