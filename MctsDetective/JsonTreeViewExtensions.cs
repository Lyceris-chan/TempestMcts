using System.Text.Json.Nodes;

namespace MctsDetective;

internal static class JsonTreeViewExtensions
{
    public static void LoadJsonToTreeView(this TreeView treeView, string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return;
        }

        var jsonNode = JsonNode.Parse(json);
        if (jsonNode is JsonObject jsonObject)
        {
            AddObjectNodes(jsonObject, "JSON", treeView.Nodes);
        }
        else if (jsonNode is JsonArray jsonArray)
        {
            AddArrayNodes(jsonArray, "JSON", treeView.Nodes);
        }
    }

    private static void AddObjectNodes(JsonObject jsonObject, string name, TreeNodeCollection parent)
    {
        var node = new TreeNode(name);
        parent.Add(node);

        foreach (var property in jsonObject)
        {
            if (property.Value != null)
            {
                AddTokenNodes(property.Value, property.Key, node.Nodes);
            }
        }
    }

    private static void AddArrayNodes(JsonArray jsonArray, string name, TreeNodeCollection parent)
    {
        var node = new TreeNode(name);
        parent.Add(node);

        for (int i = 0; i < jsonArray.Count; i++)
        {
            if (jsonArray[i] != null)
            {
                AddTokenNodes(jsonArray[i]!, $"[{i}]", node.Nodes);
            }
        }
    }

    private static void AddTokenNodes(JsonNode token, string name, TreeNodeCollection parent)
    {
        switch (token)
        {
            case JsonValue value:
                parent.Add(new TreeNode($"{name}={value}"));
                break;
            case JsonArray array:
                AddArrayNodes(array, name, parent);
                break;
            case JsonObject obj:
                AddObjectNodes(obj, name, parent);
                break;
        }
    }
}