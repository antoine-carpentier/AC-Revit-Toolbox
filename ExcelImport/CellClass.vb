Public Class CellClass


    Friend Sub New(IsMerged As Boolean, Content As String, Alignment As Integer, Width As Double, Height As Double, LocationX As Double, LocationY As Double, LeftBorder As Boolean, LeftBorderWidth As Integer, RightBorder As Boolean, RightBorderWidth As Integer, TopBorder As Boolean, TopBorderWidth As Integer, BottomBorder As Boolean, BottomBorderWidth As Integer)

        'If Not String.IsNullOrWhiteSpace(firstName) AndAlso
        'Not String.IsNullOrWhiteSpace(lastName) Then

        _IsMerged = IsMerged
        _Content = Content
        _Alignment = Alignment
        _Width = Width
        _Height = Height
        _LocationX = LocationX
        _LocationY = LocationY
        _LeftBorder = LeftBorder
        _LeftBorderWidth = LeftBorderWidth
        _RightBorder = RightBorder
        _RightBorderWidth = RightBorderWidth
        _TopBorder = TopBorder
        _TopBorderWidth = TopBorderWidth
        _BottomBorder = BottomBorder
        _BottomBorderWidth = BottomBorderWidth
        'End If

    End Sub

    Private _IsMerged As Boolean
    Public Property IsMerged() As Boolean
        Get
            Return _IsMerged
        End Get
        Set(ByVal value As Boolean)
            _IsMerged = value
        End Set
    End Property

    Private _Content As String
    Public Property Content() As String
        Get
            Return _Content
        End Get
        Set(ByVal value As String)
            _Content = value
        End Set
    End Property

    Private _Alignment As Integer
    Public Property Alignment() As Integer
        Get
            Return _Alignment
        End Get
        Set(ByVal value As Integer)
            _Alignment = value
        End Set
    End Property

    Private _Width As Double
    Public Property Width() As Double
        Get
            Return _Width
        End Get
        Set(ByVal value As Double)
            _Width = value
        End Set
    End Property

    Private _Height As Double
    Public Property Height() As Double
        Get
            Return _Height
        End Get
        Set(ByVal value As Double)
            _Height = value
        End Set
    End Property

    Private _LocationX As Double
    Public Property LocationX() As Double
        Get
            Return _LocationX
        End Get
        Set(ByVal value As Double)
            _LocationX = value
        End Set
    End Property

    Private _LocationY As Double
    Public Property LocationY() As Double
        Get
            Return _LocationY
        End Get
        Set(ByVal value As Double)
            _LocationY = value
        End Set
    End Property

    Private _TopBorder As Boolean
    Public Property TopBorder() As Boolean
        Get
            Return _TopBorder
        End Get
        Set(ByVal value As Boolean)
            _TopBorder = value
        End Set
    End Property

    Private _RightBorder As Boolean
    Public Property RightBorder() As Boolean
        Get
            Return _RightBorder
        End Get
        Set(ByVal value As Boolean)
            _RightBorder = value
        End Set
    End Property

    Private _BottomBorder As Boolean
    Public Property BottomBorder() As Boolean
        Get
            Return _BottomBorder
        End Get
        Set(ByVal value As Boolean)
            _BottomBorder = value
        End Set
    End Property

    Private _LeftBorder As Boolean
    Public Property LeftBorder() As Boolean
        Get
            Return _LeftBorder
        End Get
        Set(ByVal value As Boolean)
            _LeftBorder = value
        End Set
    End Property

    Private _LeftBorderWidth As Integer
    Public Property LeftBorderWidth() As Integer
        Get
            Return _LeftBorderWidth
        End Get
        Set(ByVal value As Integer)
            _LeftBorderWidth = value
        End Set
    End Property

    Private _TopBorderWidth As Integer
    Public Property TopBorderWidth() As Integer
        Get
            Return _TopBorderWidth
        End Get
        Set(ByVal value As Integer)
            _TopBorderWidth = value
        End Set
    End Property

    Private _RightBorderWidth As Integer
    Public Property RightBorderWidth() As Integer
        Get
            Return _RightBorderWidth
        End Get
        Set(ByVal value As Integer)
            _RightBorderWidth = value
        End Set
    End Property

    Private _BottomBorderWidth As Integer
    Public Property BottomBorderWidth() As Integer
        Get
            Return _BottomBorderWidth
        End Get
        Set(ByVal value As Integer)
            _BottomBorderWidth = value
        End Set
    End Property

End Class
