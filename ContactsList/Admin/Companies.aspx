<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="ContactsList.Admin.Companies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/admin.companies.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <asp:DropDownList ID="sortByExpression" ClientIDMode="Static" AutoPostBack="true" runat="server">
            <asp:ListItem Value="ID">ID</asp:ListItem>
            <asp:ListItem Value="Name">Название предприятия</asp:ListItem>
            <asp:ListItem Value="TownName">Название города</asp:ListItem>
            <asp:ListItem Value="ActivityName">Род деятельности</asp:ListItem>
        </asp:DropDownList>

        <asp:DataPager ID="pager"
            ClientIDMode="Static"
            runat="server"
            PagedControlID="listView"
            PageSize="10"
            class="btn-group btn-group-sm">
            <Fields>
                <asp:NextPreviousPagerField PreviousPageText="<" FirstPageText="|<" ShowPreviousPageButton="true"
                    ShowFirstPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false"
                    ButtonCssClass="btn btn-default" RenderNonBreakingSpacesBetweenControls="false" RenderDisabledButtonsAsLabels="false" />
                <asp:NumericPagerField ButtonType="Link" CurrentPageLabelCssClass="btn btn-primary disabled" RenderNonBreakingSpacesBetweenControls="false"
                    NumericButtonCssClass="btn btn-default" ButtonCount="10" NextPageText="..." NextPreviousButtonCssClass="btn btn-default" />
                <asp:NextPreviousPagerField NextPageText=">" LastPageText=">|" ShowNextPageButton="true"
                    ShowLastPageButton="true" ShowPreviousPageButton="false" ShowFirstPageButton="false"
                    ButtonCssClass="btn btn-default" RenderNonBreakingSpacesBetweenControls="false" RenderDisabledButtonsAsLabels="false" />
            </Fields>
        </asp:DataPager>

        <asp:ListView 
            DataKeyNames="ID"
            ID="listView"
            ClientIDMode="Static"
            runat="server"
            ItemType="ContactsList.Models.CompanyItemViewModel"
            SelectMethod="GetCompanies"
            UpdateMethod="UpdateCompanyName"
            DeleteMethod="RemoveCompany">

            <LayoutTemplate>
                <%--       Сортировать по:
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton runat="server"
                    ID="lnkbOrderName"
                    Text="ID"
                    CommandName="Sort"
                    CommandArgument="ID" />
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton
                    runat="server"
                    ID="lnkbAddress1"
                    Text="Названию"
                    CommandName="Sort"
                    CommandArgument="Name" />--%>
                <br />
                <br />
                <table class="table table-bordered table-striped table-selectable">
                    <thead>
                        <tr>

                            <th></th>
                            <th>Предприятие</th>
                            <th>Адрес</th>
                            <th>Деятельность</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                    </tbody>
                </table>

            </LayoutTemplate>

            <ItemTemplate>
                <tr>
                    <td>
                        <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Изменить" /></td>
                    <td><%# Item.Name %></td>
                    <td><%# Item.TownName %></td>
                    <td><%# Item.ActivityName %></td>
                    <td>
                        <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Удалить" />
                    </td>
                </tr>
            </ItemTemplate>

            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Сохранить" />
                        <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" Text="Отмена" /></td>
                    <td>
                        <asp:TextBox ID="TextBox1" Text="<%# BindItem.Name %>" runat="server"></asp:TextBox>

                    </td>
                    <td><%# Item.TownName %></td>
                    <td><%# Item.ActivityName %></td>
                    <td></td>
                </tr>
            </EditItemTemplate>

        </asp:ListView>
    </div>

</asp:Content>
