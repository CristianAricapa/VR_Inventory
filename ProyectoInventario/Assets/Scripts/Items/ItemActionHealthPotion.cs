using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionHealthPotion : ItemAction
{
    public override void Action()
    {
        if(GameManager.player.health < Constants.MAX_HEALTH)
        {
            _audioManager.PlayAudio(Constants.AudioFX.Drink_Pot);
            GameManager.player.Heal(_item.damage);
            _data.SubstractCantidad();
        }

        base.Action();
    }
}
