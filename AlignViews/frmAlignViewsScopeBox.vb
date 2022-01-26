Public Class frmAlignViewsScopeBox

    Public ScopeBoxBool As Boolean = False

    Public Function YesorNo() As Boolean

        Return ScopeBoxBool

    End Function

    Private Sub YesButton_Click(sender As Object, e As EventArgs) Handles YesButton.Click

        Close()
        ScopeBoxBool = True

    End Sub

    Private Sub NoButton_Click(sender As Object, e As EventArgs) Handles NoButton.Click

        Close()

    End Sub

End Class