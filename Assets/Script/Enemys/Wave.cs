using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

        public int Enemy1;
        public int Enemy2;
        public int Enemy3;

        public bool WaveDone()
        {
            if (Enemy1 + Enemy2 + Enemy3 == 0)
            {
                return true;
            }
            return false;
        }

        public int GetRandomEnemy()
        {
            int i = -1;
            int choode = Random.Range(0, 3);
            while (i == -1)
            {
                switch (choode)
                {
                    case 0:
                        if (Enemy1 > 0)
                        {
                            Enemy1--;
                            i = 0;
                        }
                        break;
                    case 1:
                        if (Enemy2 > 0)
                        {
                            Enemy2--;
                            i = 1;
                        }
                        break;
                    case 2:
                        if (Enemy3 > 0)
                        {
                            Enemy3--;
                            i = 2;
                        }
                        break;
                }

                choode = (choode + 1) % 3;
            }

        print(i);
            return i;
        }
    }

