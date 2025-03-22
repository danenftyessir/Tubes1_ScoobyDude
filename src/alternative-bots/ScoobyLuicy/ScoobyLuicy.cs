using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class ScoobyLuicy : Bot
{
    private Random random = new Random();
    private double targetX = -1;
    private double targetY = -1;
    private double targetDistance = double.MaxValue;
    private long lastFireTurn = 0;
    private long lastTargetTurn = -100;
    
    static void Main(string[] args)
    {
        new ScoobyLuicy().Start();
    }

    ScoobyLuicy() : base(BotInfo.FromFile("ScoobyLuicy.json")) {}
    public override void Run()
    {
        BodyColor = Color.Black;
        GunColor = Color.Red;
        RadarColor = Color.Purple;
        BulletColor = Color.Blue;
        ScanColor = Color.Yellow;
        MaxSpeed = 8;
        
        while (IsRunning)
        {
            if (TurnNumber - lastTargetTurn < 2)
            {
                double angleToTarget = GunBearingTo(targetX, targetY);
                TurnLeft(angleToTarget);  
                double gunTurn = GunBearingTo(targetX, targetY);
                TurnGunLeft(gunTurn);  
                if (TurnNumber - lastFireTurn >= 1)
                {
                    Fire(3);
                    lastFireTurn = TurnNumber;
                }
                double distance = DistanceTo(targetX, targetY);
                if (distance < 100)
                    Forward(200);
                else
                    Forward(Math.Min(150, (int)distance));
            }
            else
            {
                TurnLeft(99999);
                Forward(99999);
            }
        }
    }
    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);        
        if (distance < targetDistance)
        {
            targetX = e.X;
            targetY = e.Y;
            targetDistance = distance;
            lastTargetTurn = TurnNumber;
        }
        double gunTurn = GunBearingTo(e.X, e.Y);
        TurnGunLeft(gunTurn);
        if (TurnNumber - lastFireTurn >= 1)
        {
            Fire(3);
            lastFireTurn = TurnNumber;
        }
    }
    public override void OnHitByBullet(HitByBulletEvent e)
    {
        TurnRight(random.Next(10, 30));
        Forward(50);  
    }
    public override void OnHitBot(HitBotEvent e)
    {
        double gunTurn = GunBearingTo(e.X, e.Y);
        TurnGunLeft(gunTurn);
        Fire(3);  
        Forward(100); 
    }
    public override void OnHitWall(HitWallEvent e)
    {
        Back(180);  
        SetTurnRight(130);
    }
}
