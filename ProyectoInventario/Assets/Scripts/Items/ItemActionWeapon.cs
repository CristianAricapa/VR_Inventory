using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionWeapon : ItemAction
{
    public override void Action()
    {
        switch (_item.id)
        {
            case 0:
                if (GameManager.player.righthand != 0)
                {
                    GameManager.player.SetWeaponRight(0);
                }
                else if(GameManager.player.lefthand != 0)
                {
                    GameManager.player.SetWeaponLeft(0);
                }
                else
                {
                    GameManager.player.SetWeaponRight(0);
                }

                break;
            case 20:
                if (GameManager.player.righthand != 1)
                {
                    GameManager.player.SetWeaponRight(1);
                }
                else if (GameManager.player.lefthand != 1)
                {
                    GameManager.player.SetWeaponLeft(1);
                }
                else
                {
                    GameManager.player.SetWeaponRight(1);
                }

                break;

            default:
                break;
        }
    }


}
