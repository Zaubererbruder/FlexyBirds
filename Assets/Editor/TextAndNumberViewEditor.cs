using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
	[CustomEditor(typeof(TextAndNumberView))]
	public class TextAndNumberViewEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var obj = (TextAndNumberView)serializedObject.targetObject;
			var numberSource = serializedObject.FindProperty("_numberSource");

			EditorGUILayout.PropertyField(serializedObject.FindProperty("_text"));
			EditorGUILayout.PropertyField(numberSource);

			if (obj.NumberSource)
			{
				var propertyDesc = ComponentsProperties(obj.NumberSource, obj.NumberSourceProperty);

				if (propertyDesc != null)
				{
					obj.NumberSource = propertyDesc.component;
					obj.NumberSourceProperty = propertyDesc.name;
				}
			}

			serializedObject.ApplyModifiedProperties();
		}

		private PropertyDescription ComponentsProperties(Component component, string selectedProperty)
		{
			var components = component.GetComponents<Component>();
			var propertyNames = new List<string>();
			var properties = new List<PropertyDescription>();
			var selectedIndex = -1;

			foreach (var comp in components)
			{
				foreach (var property in comp.GetType().GetProperties())
				{
					if (property.Name == selectedProperty)
					{
						selectedIndex = propertyNames.Count;
					}

					propertyNames.Add(comp.name + "/" + property.Name);
					properties.Add(new PropertyDescription(property.Name, comp));
				}
			}

			var newSelected = EditorGUILayout.Popup("Property", selectedIndex, propertyNames.ToArray());

			if (newSelected != selectedIndex)
			{
				return properties[newSelected];
			}

			return null;
		}

		private class PropertyDescription
		{
			public PropertyDescription(string name, Component component)
			{
				this.name = name;
				this.component = component;
			}

			public string name { get; set; }
			public Component component { get; set; }
		}
	}
}