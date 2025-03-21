using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Roma3000 : Bot
{
    private Random random = new Random();
    private const int FIRE_INTERVAL = 1;
    private long lastFireTurn = 0;
    
    // Variabel untuk menyimpan target terbaik (musuh terdekat)
    private double targetX = -1;
    private double targetY = -1;
    private double targetDistance = double.MaxValue;
    private long lastTargetTurn = -100;
    
    static void Main(string[] args)
    {
        new Roma3000().Start();
    }
    
    // Ambil konfigurasi dari file JSON (misalnya Roma3000.json)
    public Roma3000() : base(BotInfo.FromFile("Roma3000.json")) {}
    
    // Karena sudah ada method DistanceTo di kelas dasar, gunakan 'new' untuk menyembunyikannya
    private new double DistanceTo(double x, double y)
    {
        double dx = x - X;
        double dy = y - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
    
    public override void Run()
    {
        // Set warna yang mencolok
        BodyColor = Color.Red;
        GunColor = Color.Black;
        RadarColor = Color.Yellow;
        BulletColor = Color.Magenta;
        ScanColor = Color.Yellow;
        
        MaxSpeed = 8;
        
        while (IsRunning)
        {
            // Jika target terdeteksi baru-baru ini (dalam 2 turn), fokus menyerang target itu
            if (TurnNumber - lastTargetTurn < 2)
            {
                double angleToTarget = GunBearingTo(targetX, targetY);
                TurnLeft(angleToTarget);  // Arahkan badan ke target
                
                double gunTurn = GunBearingTo(targetX, targetY);
                TurnGunLeft(gunTurn);       // Arahkan meriam ke target
                
                // Tembak full power
                if (TurnNumber - lastFireTurn >= FIRE_INTERVAL)
                {
                    Fire(3);
                    lastFireTurn = TurnNumber;
                }
                
                double d = DistanceTo(targetX, targetY);
                // Jika musuh sangat dekat, lakukan ramming dengan dorongan ekstra
                if (d < 100)
                    Forward(200);
                else
                    Forward(Math.Min(150, (int)d));
            }
            else
            {
                // Jika tidak ada target baru, lakukan scanning penuh dan gerak agresif untuk mencari musuh
                TurnLeft(99999);
                Forward(99999);
            }
        }
    }
    
    public override void OnScannedBot(ScannedBotEvent e)
    {
        double d = DistanceTo(e.X, e.Y);
        // Update target jika musuh yang terdeteksi lebih dekat
        if (d < targetDistance)
        {
            targetX = e.X;
            targetY = e.Y;
            targetDistance = d;
            lastTargetTurn = TurnNumber;
        }
        
        // Saat mendeteksi musuh, langsung kunci dan tembak
        double gunTurn = GunBearingTo(e.X, e.Y);
        TurnGunLeft(gunTurn);
        if (TurnNumber - lastFireTurn >= FIRE_INTERVAL)
        {
            Fire(3);
            lastFireTurn = TurnNumber;
        }
    }
    
    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Respons minimal agar tetap agresif: sedikit putar dan maju
        TurnRight(random.Next(10, 30));
        Forward(50);
    }
    
    public override void OnHitBot(HitBotEvent e)
    {
        // Saat terjadi tabrakan, langsung tembak dan terus maju menyerang
        double gunTurn = GunBearingTo(e.X, e.Y);
        TurnGunLeft(gunTurn);
        Fire(3);
        Forward(100);
    }
    
    public override void OnHitWall(HitWallEvent e)
    {
        // Jika menabrak dinding, mundur dan putar untuk keluar dari sudut sempit
        TurnLeft(90);
        Forward(100);
    }
}
