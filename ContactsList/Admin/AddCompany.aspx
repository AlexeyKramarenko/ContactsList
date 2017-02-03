<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="AddCompany.aspx.cs" Inherits="ContactsList.Admin.SaveCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Content/admin.save.company.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" ClientIDMode="Static" runat="server">
            <ContentTemplate>
                <table id="companyLocation" style="float: left;">
                    <tr>
                        <td></td>
                        <td>
                            <asp:ListView ID="listView"
                                OnItemCommand="listView_ItemCommand"
                                DataKeyNames="ID"
                                ItemType="ContactsList.Admin.Models.TownViewModel"
                                runat="server">
                                <LayoutTemplate>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li>
                                        <asp:ImageButton ID="ImageButton1"
                                            CssClass="deleteBtn"
                                            ImageUrl="~/Content/delete.jpg"
                                            CommandArgument="<%# BindItem.ID  %>"
                                            CommandName="DeleteTown"
                                            runat="server" />
                                        <%# Item. Name %>
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                    <tr>
                        <td>Страна:
                    <asp:DropDownList ID="ddlCountry"
                        EnableViewState="false"
                        AutoPostBack="true"
                        ItemType="ContactsList.Admin.Models.CountryViewModel"
                        SelectMethod="GetCountries"
                        DataTextField="Name"
                        DataValueField="ID"
                        runat="server">
                    </asp:DropDownList>
                        </td>

                        <td>Город:
                    <asp:DropDownList ID="ddlTown"
                        EnableViewState="false"
                        ItemType="ContactsList.Admin.Models.TownViewModel"
                        SelectMethod="GetTowns"
                        DataTextField="Name"
                        DataValueField="ID"
                        runat="server">
                    </asp:DropDownList>
                            <br />
                            <br />
                            <asp:LinkButton ID="addTownBtn" OnClick="AddTown_Click" runat="server">+Добавить этот город</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" ClientIDMode="Static" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblError" runat="server"></asp:Label><br />
                <asp:FormView
                    ID="formView"
                    DataKeyNames="ID"
                    DefaultMode="Edit"
                    runat="server"
                    SelectMethod="InitFormView"
                    UpdateMethod="Save"
                    RenderOuterTable="false"
                    ItemType="ContactsList.Admin.Models.AddCompanyViewModel">
                    <EditItemTemplate>
                        <table id="addCompanyForm">
                            <tbody>
                                <tr>
                                    <td>Название:</td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyName" runat="server" Text="<%# BindItem.Name %>" /></td>

                                    <asp:ModelErrorMessage
                                        ID="FirstNameErrorMessage"
                                        ModelStateKey="Name"
                                        ForeColor="Red"
                                        runat="server" />

                                    <asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1"
                                        ControlToValidate="txtCompanyName"
                                        runat="server"
                                        ValidationExpression="^.{0,100}$"
                                        ErrorMessage="Текст не должен превышать 100 символов"></asp:RegularExpressionValidator>
                                </tr>
                                <tr>
                                    <td>Деятельность:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlActivity"
                                            DataSource="<%# Item.Activities %>"
                                            DataTextField="Name"
                                            DataValueField="ID"
                                            SelectedIndex="<%# BindItem.ActivityID %>"
                                            runat="server">
                                        </asp:DropDownList>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="saveBtn" 
                                            ClientIDMode="Static" 
                                            CssClass="btn btn-default" 
                                            CommandName="Update" 
                                            runat="server" 
                                            Text="Сохранить" />
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </EditItemTemplate>
                </asp:FormView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
