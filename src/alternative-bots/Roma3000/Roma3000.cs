using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Roma3000 : Bot
{
    // Informasi target
    private double targetX;
    private double targetY;
    private double targetDistance = double.MaxValue;
    private double targetBearing = 0;
    private bool hasTarget = false;

    // Variabel bantu gerakan menghindar
    private int moveDist = 50;
    private Random random = new Random();

    static void Main(string[] args)
    {
        new Roma3000().Start();
    }

    Roma3000() : base(BotInfo.FromFile("Roma3000.json")) { }

    public override void Run()
    {
        // Warna-warna bot
        BodyColor = Color.FromArgb(0x00, 0xFF, 0x00);       // hijau
        GunColor = Color.FromArgb(0x00, 0x00, 0x00);        // hitam
        TurretColor = Color.FromArgb(0x00, 0x00, 0x00);     // hitam
        RadarColor = Color.FromArgb(0xFF, 0xFF, 0x00);      // kuning
        ScanColor = Color.FromArgb(0xFF, 0xFF, 0x00);       // kuning
        BulletColor = Color.FromArgb(0xFF, 0x00, 0xFF);     // ungu/merah kebiruan

        // Loop utama
        while (IsRunning)
        {
            if (hasTarget)
            {
                // Cari sudut turret dan body
                double gunTurn = NormalizeRelativeAngle(targetBearing - GunDirection);
                TurnGunLeft(gunTurn);

                // Bisa diatur: apakah selalu mengarahkan body ke target?
                double bodyTurn = NormalizeRelativeAngle(targetBearing - Direction);
                // Jika jarak cukup dekat, mungkin tidak usah selalu hadap depan
                if (targetDistance > 100)
                {
                    TurnLeft(bodyTurn);
                }

                // Dekati target, tapi pertahankan jarak minimal (misal 80)
                double desiredDistance = 80;
                double distanceToMove = targetDistance - desiredDistance;
                if (distanceToMove > 0)
                    Forward(distanceToMove);

                // Tembak
                double myEnergy = Energy;
                double firePower = Math.Min(400 / Math.Max(targetDistance, 1), 3);

                // Sesuaikan firepower dengan energi kita
                if (myEnergy < 20 && firePower > 1)
                    firePower = 1; // hemat energi

                Fire(firePower);
            }
            else
            {
                // Jika belum ada target, putar radar/gun untuk scan
                TurnGunLeft(20);
            }

            // Selalu lakukan rescan agar event OnScannedBot dipanggil
            Rescan();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        // Anda bisa pakai kriteria lain, misal hitung "score" lawan
        double distance = DistanceTo(e.X, e.Y);
        if (distance < targetDistance)
        {
            // Update target
            targetDistance = distance;
            targetBearing = DirectionTo(e.X, e.Y);
            targetX = e.X;
            targetY = e.Y;
            hasTarget = true;
        }
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {
        // Menghindar dengan gerakan acak
        // Sudut menghindar ± 90 derajat dari arah peluru
        double dodgeAngle = 90 + random.Next(-30, 31); // 90 ± [0..30]
        TurnLeft(NormalizeRelativeAngle(dodgeAngle - (Direction - e.Bullet.Direction)));

        // Maju atau mundur acak
        int dodgeDistance = random.Next(50, 150);
        Forward(dodgeDistance);

        // Bisa balik arah
        moveDist *= -1;
        Rescan();
    }

    public override void OnHitBot(HitBotEvent e)
    {
        double targetDir = DirectionTo(e.X, e.Y);
        double gunTurn = NormalizeRelativeAngle(targetDir - GunDirection);
        TurnGunLeft(gunTurn);
        Fire(3); // Tembakan kuat saat tabrakan

        // Bisa mundur sedikit agar tidak terus menempel
        Back(30);

        Rescan();
    }

    // (Opsional) Tangani jika target “hilang” atau mati.
    // Bisa dipanggil misalnya saat OnBotDeath, dsb.
    public override void OnBotDeath(BotDeathEvent e)
    {
        // Jika bot yang mati adalah target kita, reset
        if (hasTarget && IsSameTarget(e))
        {
            ResetTarget();
        }
    }

    private bool IsSameTarget(BotDeathEvent e)
    {
        // Di TankRoyale, Anda dapat membandingkan id Bot yang mati dengan id yang Anda simpan
        // (Simpan juga ID target di OnScannedBot). Kode di sini hanya gambaran.
        return false; 
    }

    private void ResetTarget()
    {
        targetDistance = double.MaxValue;
        hasTarget = false;
    }
}
