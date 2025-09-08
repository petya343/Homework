using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TakingDamage
{
    private Movement movement;
    private GameObject player;
    private ScreenShaker mockShaker;
    private Hearts mockHearts;

    [SetUp]
    public void Setup()
    {
        player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        movement = player.GetComponent<Movement>();

        //mockShaker = Substitute.For<IScreenShaker>();
        //mockHearts = Substitute.For<IHearts>();

        movement.SetScreenShaker(mockShaker);
        movement.SetHeartsUI(mockHearts);
    }

    [Test]
    public void TakeDamage_DecreasesLivesByOne()
    {
        int initialLives = movement.LivesCounter();

        movement.takeDamage();

        int newLives = movement.LivesCounter();
        Assert.AreEqual(initialLives - 1, newLives);

        mockShaker.Received(1).ScreenShake();
        mockHearts.Received(1).SetLives(newLives);
    }
     
}
