# Show Attribute

조건을 통과하는 항목에 대해서만 인스펙터에서 노출하기 위한 어트리뷰트 입니다.

### 설치방법
1. 패키지 관리자의 툴바에서 좌측 상단에 플러스 메뉴를 클릭합니다.
2. 추가 메뉴에서 Add package from git URL을 선택하면 텍스트 상자와 Add 버튼이 나타납니다.
3. https://github.com/DarkNaku/Attribute_Show.git 를 입력하고 Add를 클릭합니다.

### 사용방법
```csharp
    // 조건이 될 bool 필드
    [SerializeField] 
    private bool showAdvancedSettings;

    [SerializeField] 
    private bool _isEnabled = true;

    // 조건이 될 bool 속성
    public bool IsEnabled
    {
        get { return _isEnabled; }
    }

    // 조건이 될 메서드 (파라미터 없음)
    private bool ShouldShowDetails()
    {
        return _isEnabled && showAdvancedSettings;
    }

    // 조건이 될 메서드 (파라미터 있음)
    private bool IsGreaterThan(int value)
    {
        return value > 10;
    }

    // 조건이 될 메서드 (파라미터 있음)
    private bool IsStringMatch(string input)
    {
        return input == "Show";
    }

    // 조건이 될 메서드 (파라미터 두개)
    private bool CombineConditions(bool condition1, bool condition2)
    {
        return condition1 && condition2;
    }

    [Show("showAdvancedSettings")]
    public float acceleration;

    [Show("IsEnabled")]
    public float drag;

    [Show("ShouldShowDetails")]
    public float torque;

    [Show("IsGreaterThan", 15)]
    public float heavyAcceleration;

    [Show("IsStringMatch", "Show")]
    public float specialForce;

    [Show("CombineConditions", true, true)]
    public float combinedForce;
```