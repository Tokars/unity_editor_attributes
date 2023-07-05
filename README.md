# Custom unity attributes.

### Inspector:

- **[RequiredField]** works with references types, logs error if required field isn't set.
  ```
  [SerializedField, RequiredField] Transform;
  ```

- **[ShowIf]** Conditional attribute hide, disable field by condition.
  ```
  [SerializeField] private bool bool01;
  [SerializeField] private int int01;
  [SerializeField, ShowIf(ActionOnConditionFail.DO_NOT_DRAW, ConditionOperator.OR, new string[] {nameof(bool01), nameof(int01)})]
  private string str;
  ```

- **[PreviewSprite]** Display sprite preview in inspector.
  ```
  [SerializeField, PreviewSprite(100)] private Sprite sprite;
  ```
- **[EditorButton] and [ScriptableEditorButton]** Simple method attribute (without params). Display simple inspector button.
