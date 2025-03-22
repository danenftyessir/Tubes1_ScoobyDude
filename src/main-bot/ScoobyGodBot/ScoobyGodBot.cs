using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class ScoobyGodBot : Bot
{   
    static void Main(string[] args)
    {
        new ScoobyGodBot().Start();
    }

    ScoobyGodBot() : base(BotInfo.FromFile("ScoobyGodBot.json")) { }

    public override void Run()
    {
        BodyColor = Color.Brown;
        TurretColor = Color.Red;
        BulletColor = Color.Purple;

        while (IsRunning)
        {
            AdaptiveMovement();
            SetTurnGunRight(10);
        }
    }

    private void AdaptiveMovement()
    {
        if (Energy > 70) //aggressive saat energi penuh
        {
            SetTurnRight(30);
            Forward(200);
        } else if (Energy > 30) //balanced
        {
            SetTurnRight(60);
            Forward(100);
        } else  //defensive saat energy rendah
        {
            SetTurnLeft(90);
            Back(150);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        
        if (distance < 150) //jika jarak dekat
        {
            Fire(3);
        } else if (distance < 500) //jika jarak sedang
        {
            Fire(2);
        } else //jika jarak jauh
        {
            Fire(3);
        }
    }

    public override void OnHitBot(HitBotEvent e)
    {
        if (e.IsRammed && Energy > 20) //jika bot ditabrak/menabrak dan energi masih cukup
        {
            Fire(3);
            Forward(50);
        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        Back(150);
        SetTurnRight(120);
    }
}
