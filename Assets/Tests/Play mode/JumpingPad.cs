using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{ 
    public class JumpingPad
    {
        private GameObject player;
        private GameObject jumpingPad;

        [SetUp]
        public void SetUp()
        {
            GameObject cam = new GameObject("Main Camera");
            Camera camera = cam.AddComponent<Camera>();
            cam.tag = "MainCamera"; 
            cam.AddComponent<ScreenShaker>(); 

            player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
            jumpingPad = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Platform Variant 2"));
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(player);
            Object.Destroy(jumpingPad);
        }

        [UnityTest]
        public IEnumerator JumpingPadMovement()
        {
            jumpingPad.transform.position = Vector3.zero;

            player.transform.position = new Vector3(jumpingPad.transform.position.x + 1f,
                                                    jumpingPad.transform.position.y + 2f,
                                                    jumpingPad.transform.position.z);

            float firstY = player.transform.position.y;
            yield return new WaitForSeconds(1f);
            float finalY = player.transform.position.y;
            Assert.Greater(finalY, firstY, "Player's Y position should increase after colliding with the jumping pad");
 
        }
    }
}
