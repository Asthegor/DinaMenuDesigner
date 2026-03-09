using DinaMenuDesigner.Models;

namespace DinaMenuDesigner.Services
{
    public class GenerateCodeService
    {
        public static string GenerateDeclaration(MenuManagerModel menuManagerModel)
        {
            string generatedCode = "private MenuManager CreateMenu()\n{\n";

            // Construction du MenuManager
            var itemSpacing = $"itemspacing: new Vector2({menuManagerModel.SpacingX}, {menuManagerModel.SpacingY})";
            var direction = $"direction: MenuItemDirection.{menuManagerModel.Direction}";
            var constructorParams = string.Join(", ", new[] { itemSpacing, direction});

            generatedCode += $"var menuManager = new MenuManager({constructorParams});\n";

            // Ajout des titres
            int indexTitle = 1;
            foreach(var title in menuManagerModel.Titles)
            {
                var font = $"font: {title.PlaceholderFont}";
                var text = $"text: \"{title.Content}\"";
                var position = $"position: new Vector2({title.PositionX}, {title.PositionY})";
                var color = $"color: new Color({title.Color.R}, {title.Color.G}, {title.Color.B}, {title.Color.A})";
                var shadowcolor = $"shadowcolor: new Color({title.ShadowColor.R}, {title.ShadowColor.G}, {title.ShadowColor.B}, {title.ShadowColor.A})";
                var shadowoffset = $"shadowoffset: new Vector2({title.ShadowOffsetX}, {title.ShadowOffsetY})";
                var zorder = $"zorder: {title.ZOrder}";

                var addTitleParams = string.Join(", ", new[] { font, text, position, color });
                if (title.HasShadow)
                    addTitleParams = string.Join(", ", new[] { addTitleParams, shadowcolor, shadowoffset });
                addTitleParams = string.Join(", ", new[] { addTitleParams, zorder });

                generatedCode += $"var title{indexTitle} = menuManager.AddTitle({addTitleParams});\n";

                if (!title.Visible)
                    generatedCode += $"title{indexTitle}.Visible = false;\n";

                indexTitle++;
            }

            generatedCode += "\n";

            // Ajout des items du menu
            int indexItem = 1;
            foreach(var item in menuManagerModel.Items)
            {
                var font = $"font: {item.PlaceholderFont}";
                var text = $"text: \"{item.Content}\"";
                var color = $"color: new Color({item.Color.R}, {item.Color.G}, {item.Color.B}, {item.Color.A})";
                var selection = string.IsNullOrWhiteSpace(item.SelectionMethodName) == false ? $"selection: {item.SelectionMethodName}" : string.Empty;
                var deselection = string.IsNullOrWhiteSpace(item.DeselectionMethodName) == false ? $"deselection: {item.DeselectionMethodName}" : string.Empty;
                var activation = string.IsNullOrWhiteSpace(item.ActivationMethodName) == false ? $"activation: {item.ActivationMethodName}" : string.Empty;
                var halign = $"halign: HorizontalAlignment.{item.HorizontalAlignment}";
                var valign = $"valign: VerticalAlignment.{item.VerticalAlignment}";
                var addItemParams = string.Join(", ", new[] { font, text, color, selection, deselection, activation, halign, valign }.Where(p => !string.IsNullOrEmpty(p)));

                generatedCode += $"var item{indexItem} = menuManager.AddItem({addItemParams});\n";

                generatedCode += $"item{indexItem}.Position = new Vector2({item.PositionX}, {item.PositionY});\n";

                if (!item.IsEnabled)
                {
                    generatedCode += $"item{indexItem}.State = MenuItemState.Disable;\n";
                    generatedCode += $"item{indexItem}.DisableColor = new Color({item.DisableColor.R}, {item.DisableColor.G}, {item.DisableColor.B}, {item.DisableColor.A});\n";
                }
                if(!item.Visible)
                    generatedCode += $"item{indexItem}.Visible = false;\n";

                indexItem++;
            }

            generatedCode += "return menuManager;\n}\n";

            return generatedCode;
        }
    }
}
