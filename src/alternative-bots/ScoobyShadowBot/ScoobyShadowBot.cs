using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class ScoobyShadowBot : Bot
{
    static void Main(string[] args)
    {
        new ScoobyShadowBot().Start();
    }

    ScoobyShadowBot() : base(BotInfo.FromFile("ScoobyShadowBot.json")) { }

    public override void Run()
    {
        BodyColor = Color.Black;
        GunColor = Color.Red;
        RadarColor = Color.Purple;
        BulletColor = Color.Blue;
        
        while (IsRunning)
        {
            EvadeAndScan();
        }
    }
    
    //bot bergerak dengan cara zigzag untuk menghindar dari tembakan dan agar scan musuh lebih optimal
    private void EvadeAndScan() 
    {
        SetTurnRight(45);
        Forward(160);
        SetTurnLeft(90);
        Forward(160);
        SetTurnGunRight(45);
    }
    
    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        
        if (distance > 400) //jarak jauh tembak dengan power penuh ketika meriam tidak panas
        {
            if (GunHeat == 0)
            {
                Fire(3);
            }
        } else if (distance > 200) //jarak menengah
        {
            Fire(2);
        } else //jarak dekat tembak dengan power penuh
        {
            SetTurnRight(90);
            Back(100);
            Fire(3);
        }
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    { //saat bot tertebak, bot belok dan maju untuk menjauh
        SetTurnRight(60);
        Forward(150);
    }

    public override void OnHitWall(HitWallEvent e)
    { //saat bot nabrak dinding, bot belok dan mundur
        Back(180);
        SetTurnRight(130);
    }
}
