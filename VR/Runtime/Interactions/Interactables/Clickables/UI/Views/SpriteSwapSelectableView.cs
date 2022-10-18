using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.VR
{

    public class SpriteSwapSelectableView : BaseUIInteractableView
    {

        private Selectable selectable;
        private Selectable Selectable => selectable ??= GetComponent<Selectable>();

        private Sprite enabledSprite;
        private Image Image => Selectable.targetGraphic as Image;

        private void SetEnabledSprite()
        {
            if (enabledSprite == null)
                enabledSprite = Image.sprite;
        }

        protected override void OnClick()
        {
            if (Image == null)
                return;
            SetEnabledSprite();
            Image.sprite = Selectable.spriteState.pressedSprite;
        }

        protected override void OnDisabled()
        {
            if (Image == null)
                return;
            SetEnabledSprite();
            Image.sprite = Selectable.spriteState.disabledSprite;
        }

        protected override void OnEnabled()
        {
            if (Image == null)
                return;
            SetEnabledSprite();
            Image.sprite = enabledSprite;
        }

        protected override void OnHover()
        {
            if (Image == null)
                return;
            SetEnabledSprite();
            Image.sprite = Selectable.spriteState.selectedSprite;
        }

    }

}