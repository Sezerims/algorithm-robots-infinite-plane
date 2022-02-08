using System;
using System.Threading;

namespace fieldscope
{
    class Robot
    {
        private int _number; // Robotun numarası
        private int _position; // Robotun güncel pozisyonu
        private static int _parachute; // Paraşütün pozisyonu, robotlar iniş yaptığında atanır ve sabittir.
        private bool _parachuteDetected = false; // Diğer robotun paraşütünün bulunmuş olmasını kontrol eden bool
        private bool _robotDetected = false; // Diğer robotun bulunmuş olmasını kontrol eden bool
        
        public void Constructor(int robotNumber, int landingPosition) // Parametre olarak robot objesinin numarasını ve iniş konumunu alan consturcor
        {
            _number = robotNumber;
            _position = landingPosition;
            _parachute = landingPosition;
        }

        private void MoveForward(int units = 1) { // Robotu, girilen birim kadar ilerleten metod
            _position += units;
        }

        private int LeaveParachute() // Paraşütün konumunu (statik) döndüren metod
        {
            return _parachute;
        }

        private int MarkSpot() // Robotun konumunu (dinamik) döndüren metod
        {
            return _position;
        }

        // Robotların hareketini tanımlayan metod. Parametre olarak diğer robotu alıyor.
        // Buradaki amaç diğer robotun paraşütünü veya kendisini 1 birim mesafeden saptayabilmek.
        public void Search(Robot otherRobot)
        {
            Console.Write("Robot " + _number + " starts at position: " + _position + "\n");

            // Diğer robot bulunana kadar devam eden döngü
            while (!_robotDetected)
            {
                // Bir robotu, bir paraşütle karşılaşana kadar 1 birim, sonrasında 2 birim ilerletiyoruz.
                if (!_parachuteDetected)
                {
                    MoveForward();
                }
                else
                {
                    MoveForward(2);
                }

                // Döngünün her tekrarında paraşüt için 1 birim ilerisini ve diğer robot için 1 birim çapını kontrol ediyoruz.
                
                // Paraşüt için sadece ileriyi kontrol etmek yeterli çünkü paraşüt bulunana kadar robot 1 birim ilerliyor.
                if (_position == otherRobot.LeaveParachute() && !_parachuteDetected)
                {
                    Console.WriteLine("Robot " + _number + " detects a parachute at position: " + otherRobot.LeaveParachute());
                    _parachuteDetected = true;
                }
                
                // Diğer robot için 1 birim çapı kontrol ediyoruz çünkü robot, 2 birim ilerlerken, diğer robotun konumunu geçebilir.
                if (_position == otherRobot.MarkSpot() - 1 || _position == otherRobot.MarkSpot() || _position == otherRobot.MarkSpot() + 1)
                {
                    Console.WriteLine("Robot " + _number + " detects another robot at position: " + otherRobot.MarkSpot());
                    Console.WriteLine("Robot " + _number + " stops at position: " + _position);
                    _robotDetected = true;
                }
            }
        }
        
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Negatif sonsuz ve pozitif sonsuz sayılar aralığında rastgele birer sayı üretiyoruz.
            // Test için daha küçük bir aralık belirliyoruz.
            int negInf = Int32.MinValue;
            int posInf = Int32.MaxValue;

            Random p1 = new Random();
            int p1Inf = p1.Next(negInf, posInf);
            int p1Test = p1.Next(-5, 5);
            
            Random p2 = new Random();
            int p2Inf = p2.Next(negInf, posInf);
            int p2Test = p1.Next(-5, 5);
            
            // robot1 ve robot2 objelerini oluşturup numaralarını ve rastgele iniş konumlarını atıyoruz.
            Robot robot1 = new Robot();
            Robot robot2 = new Robot();
            
            robot1.Constructor(1, 0);
            robot2.Constructor(2, 13);

            // TEST
            // İki robotu da aynı anda hareket ettirebilmek için multithreading kullandım. Bu, beklenmedik sonuçlar döndürebiliyor.
            // Asenkron programlama daha iyi bir test yöntemi olurdu ancak şu anda hakim olduğum bir konu değil.
            Thread thread1 = new Thread(() => robot1.Search(robot2));
            Thread thread2 = new Thread(() => robot2.Search(robot1));
            
            thread1.Start();
            thread2.Start();
        }
    }
}