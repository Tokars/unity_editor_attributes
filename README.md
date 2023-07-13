## Custom unity attributes. [![openupm](https://img.shields.io/npm/v/tokar.dev.attributes?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/tokar.dev.attributes/)

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
- **[MinMaxSlider(0, 10)]** Simple min. / max. inspector Vector2 based slider.
- **[ReadOnly]** Display disabled/not active field.