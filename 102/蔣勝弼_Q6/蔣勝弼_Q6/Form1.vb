Public Class Form1
    Dim lblText(9) As Label
    Dim box(1, 9) As TextBox
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i = 0 To 9
            For q = 0 To 1
                box(q, i) = New TextBox
                With box(q, i)
                    .Location = New Point(160 + q * 60, 10 + i * 30)
                    .Size = New Size(50, 22)
                    .Visible = False
                End With
                Panel1.Controls.Add(box(q, i))
            Next
            lblText(i) = New Label()
            With lblText(i)
                .Location = New Point(10, 15 + i * 30)
                .Text = "m" & (i + 1) & "的矩陣大小: "
                .Visible = False
            End With
            Panel1.Controls.Add(lblText(i))
        Next
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim index As Integer = Val(TextBox1.Text)
        If index >= 3 And index <= 10 Then
            index -= 1
            For i = 0 To 9
                box(0, i).Visible = (i <= index)
                box(1, i).Visible = (i <= index)
                lblText(i).Visible = (i <= index)
            Next
        Else
            For i = 0 To 9
                box(0, i).Visible = False
                box(1, i).Visible = False
                lblText(i).Visible = False
            Next
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim N As Integer = Val(TextBox1.Text) - 1
        If N < 2 Or N > 9 Then MsgBox("輸入的矩陣數必須在3~10之間。", MsgBoxStyle.OkOnly, "提示") : Exit Sub
        Dim R(N) As Rect
        Dim Record As String = ""
        Dim BestTimes As Integer = Integer.MaxValue
        '//Get Value
        For i = 0 To N
            R(i).X = Val(box(0, i).Text)
            R(i).Y = Val(box(1, i).Text)
            R(i).Num = i + 1
        Next
        '//DFS
        Dim newRect() As Rect : Dim Pos As Integer
        For i = 0 To N - 1
            For q = i + 1 To N
                If R(i).Y = R(q).X Then
                    Dim NowRect As Rect : NowRect.Setting(R(i).X, R(q).Y, -1)
                    ReDim newRect(N - 2) : Pos = -1
                    For k = 0 To N
                        If k <> i And k <> q Then
                            Pos += 1
                            newRect(Pos) = R(k)
                        End If
                    Next
                    DFS(BestTimes, Record, newRect, NowRect, R(i).X * R(i).Y * R(q).Y, "<m" & R(i).Num & " m" & R(q).Num & ">")
                ElseIf R(i).X = R(q).Y Then
                    Dim NowRect As Rect : NowRect.Setting(R(i).Y, R(q).X, -1)
                    ReDim newRect(N - 2) : Pos = -1
                    For k = 0 To N
                        If k <> i And k <> q Then
                            Pos += 1
                            newRect(Pos) = R(k)
                        End If
                    Next
                    DFS(BestTimes, Record, newRect, NowRect, R(i).Y * R(i).X * R(q).X, "<m" & R(i).Num & " m" & R(q).Num & ">")
                End If
            Next
        Next
        '//Output
        Label4.Text = "矩陣相乘的次序為: " & Record
        Label3.Text = "最少的乘法運算次數: " & BestTimes
    End Sub
    Sub DFS(ByRef BestTimes As Integer, ByRef Record As String, ByVal Rects() As Rect, ByVal NowRect As Rect, ByVal Times As Integer, ByVal Recording As String)
        If IsNothing(Rects) Then
            If Times < BestTimes Then
                BestTimes = Times
                Record = Recording
            End If
        Else
            For i = 0 To UBound(Rects)
                If NowRect.Y = Rects(i).X Then
                    Dim New_NowRect As Rect : New_NowRect.Setting(NowRect.X, Rects(i).Y, -1)
                    Dim newRects() As Rect : Dim Pos As Integer = -1
                    For k = 0 To UBound(Rects)
                        If k <> i Then
                            Pos += 1
                            ReDim Preserve newRects(Pos)
                            newRects(Pos) = Rects(k)
                        End If
                    Next
                    DFS(BestTimes, Record, newRects, New_NowRect, Times + NowRect.X * NowRect.Y * Rects(i).Y, "<" & Recording & " m" & Rects(i).Num & ">")
                ElseIf NowRect.X = Rects(i).Y Then
                    Dim New_NowRect As Rect : New_NowRect.Setting(NowRect.Y, Rects(i).X, -1)
                    Dim newRects() As Rect : Dim Pos As Integer = -1
                    For k = 0 To UBound(Rects)
                        If k <> i Then
                            Pos += 1
                            ReDim Preserve newRects(Pos)
                            newRects(Pos) = Rects(k)
                        End If
                    Next
                    DFS(BestTimes, Record, newRects, New_NowRect, Times + NowRect.Y * NowRect.X * Rects(i).X, "<" & Recording & " m" & Rects(i).Num & ">")
                End If
            Next
        End If
    End Sub
End Class
Public Structure Rect
    Public X As Integer
    Public Y As Integer
    Public Num As Integer
    Public Sub Setting(Xr As Integer, Yr As Integer, Num As Integer)
        X = Xr : Y = Yr
    End Sub
End Structure
