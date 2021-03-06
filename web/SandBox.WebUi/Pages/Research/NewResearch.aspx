﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="NewResearch.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.NewResearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	    <div id="content-top">
		    <div id="pagename">Создание исследования</div>
</div>
   <div id="content-main" style="height:700px;">
<table class='panel'>
			<tbody>
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Назад к списку исследований" NavigateUrl="~/Pages/Research/Current.aspx">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
			</tbody>
</table>


<div style="float:left; width:35%;">
    <div class="mainparams">Основные параметры исследования</div>
<div id="mainprmcont">
<table class='form'>
			<tbody>
                <tr>
                  <td style="width: 150px"><div class="table_text">Наименование исследования:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbLir" runat="server" Width="200px" ValidationSettings-ErrorTextPosition="Bottom">
                         <ValidationSettings ValidationGroup="AddResearchValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxTextBox>
                      <dx:ASPxLabel ID="LNameValidation" runat="server" Visible="False">
                      </dx:ASPxLabel>
                  </td>
                </tr>
                <tr>
                  <td style="width: 150px"><div class="table_text">ЛИР:</div></td>
                  <td>
                       <dx:ASPxComboBox ID="cbMachine" runat="server" Width="200px" 
                           LoadingPanelText="Загрузка&amp;hellip;">
                        <ValidationSettings ValidationGroup="AddResearchValidationGroup" 
                               ErrorTextPosition="Bottom">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                    </ValidationSettings>
                       </dx:ASPxComboBox>
                        <dx:ASPxHyperLink ID="linkRegisterNewVm" 
                           ImageUrl ="../../Content/Images/Icons/add_lir.png" runat="server" 
                           Text="Добавить ЛИР" NavigateUrl="~/Pages/Information/CreateEtalonMachine.aspx" 
                           Visible="False">
                        </dx:ASPxHyperLink>
                      
                        <dx:ASPxHyperLink ID="linkCreateNewVm"  
                           ImageUrl ="../../Content/Images/Icons/add_vlir.png" runat="server" 
                           Text="Создать ВЛИР" NavigateUrl="~/Pages/Information/CreateMachine.aspx" 
                           Visible="False">
                        </dx:ASPxHyperLink>
                      
                  </td>
                </tr>
                <tr>
                  <td style="width: 150px"><div class="table_text">Наименование ВПО:</div></td>
                  <td>
                       <dx:ASPxComboBox ID="cbMalware" runat="server" Width="200px" 
                           LoadingPanelText="Загрузка&amp;hellip;">
                       <ValidationSettings ValidationGroup="AddResearchValidationGroup" 
                               ErrorTextPosition="Bottom">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                    </ValidationSettings>
                       </dx:ASPxComboBox>
                  </td>
                </tr>
                <tr>
                  <td style="width: 150px"><div class="table_text">Время проведения:</div></td>
                  <td>
                        <dx:ASPxSpinEdit ID="spinTime" runat="server" Height="21px" Number="0" 
                            Width="200px" MinValue = "1" MaxValue = "120">
<ValidationSettings ErrorText="Неверное значение" ErrorTextPosition="Bottom">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
                        </dx:ASPxSpinEdit>
                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="(в минутах)">
                        </dx:ASPxLabel>
                  </td>  
                </tr>
                
			<%--</tbody>--%>
            <%--<tr>
                  <td style="width: 150px"><div class="table_text">Скрыть файл:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbHideFile" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>--%>
            <%--<tr>
                  <td style="width: 150px"><div class="table_text">Запретить удаление файла:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbLockDelete" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>--%>
            <%-- <tr>
                  <td style="width: 150px"><div class="table_text">Скрыть ветку реестра:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbHideRegistry" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>--%>
            <%--  <tr>
                  <td style="width: 150px"><div class="table_text">Скрыть процесс:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbHideProcess" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>--%>
            <%--    <tr>
                  <td style="width: 150px"><div class="table_text">Установить полосу пропускания:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbSetBandwidth" runat="server" Width="200px" Text="4096">
                           <MaskSettings IncludeLiterals="DecimalSymbol" />
                       </dx:ASPxTextBox>
                  </td>
                </tr>--%>
            <tr>
                <td style="width: 150px">
                    <div class="table_header">
                        Завершение по наступлению события</div>
                </td>
                <td>
                    <%--<hr style="width:210px;"/>--%>
                    <dx:ASPxCheckBox ID="CbStopEvent" runat="server" 
                        oncheckedchanged="CbStopEvent_CheckedChanged" 
                        Text="Включить остановку по событию" AutoPostBack="True" Checked="True" 
                        CheckState="Checked">
                    </dx:ASPxCheckBox>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    <div class="table_text">
                        Модуль:</div>
                </td>
                <td>
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" AutoPostBack="True" 
                        LoadingPanelText="Загрузка&amp;hellip;" 
                        onselectedindexchanged="ASPxComboBox1_SelectedIndexChanged" Width="200px">
                        <Items>
                            <dx:ListEditItem Text="Сетевая активность" Value="Сетевая активность" />
                            <dx:ListEditItem Text="Файловая система" Value="Файловая система" />
                            <dx:ListEditItem Text="Реестр" Value="Реестр" />
                            <dx:ListEditItem Text="Процессы" Value="Процессы" />
                        </Items>
                        <ValidationSettings ErrorText="Неверное значение" ErrorTextPosition="Bottom">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </td>
            </tr>
                <tr>
                    <td style="width: 150px">
                        <div class="table_text">
                            Событие:</div>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ASPxComboBox4" runat="server" ValueType="System.String" 
                            Width="200px">
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        <div class="table_text">
                            Источник:</div>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" 
                            ontextchanged="ASPxTextBox1_TextChanged" Width="200px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">
                        <div class="table_text">
                            Адресат:</div>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="200px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            <tr>
                <td style="width: 150px">
                    <div class="table_text">
                    </div>
                </td>
                <td>
                    <dx:ASPxButton ID="btnCreate" runat="server" AutoPostBack="false" 
                        onclick="BtnCreateClick" Text="Создать" 
                        ValidationGroup="AddResearchValidationGroup">
                    </dx:ASPxButton>
                </td>
            </tr>
            </tbody>
            <%--</caption>--%>
</table>  
</div>
</div>     
<div style="float: left; padding-left: 30px; width:60%;">
    <div class="addparams">Дополнительные параметры исследования</div>
    <ajaxToolkit:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" CssClass="accordion" FadeTransitions="False">
    <Panes>  
    <ajaxToolkit:AccordionPane runat="server">  
        <Header><div id="accfs"></div><div class="acchdr">Файловая система</div></Header>  
        <Content>  
        <dx:ASPxGridView ID="ASPxGridView2" runat="server" Width="100%" 
            style="margin-bottom: 5px; margin-top:5px;" AutoGenerateColumns="False" 
            KeyFieldName="f2">
            <Columns>
                <dx:GridViewDataTextColumn Caption="Задание" FieldName="f1" VisibleIndex="0" 
                    Width="300px">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Параметр" FieldName="f2" VisibleIndex="1">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsPager Visible="False">
            </SettingsPager>
            <SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
        </dx:ASPxGridView>               
        <table style="width: 100%;">
            <tr>
                <td style="width: 210px">
                   <dx:ASPxComboBox ID="CBFileActiv" runat="server" ValueType="System.String" 
                        Width="200px">
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 262px">
                    <dx:ASPxTextBox ID="TBNFileTaskValue" runat="server" Width="250px" 
                        style="left:210px;" Height="20px">
                    </dx:ASPxTextBox>
                </td>
                <td style="width: 80px">
                    <dx:ASPxButton ID="BAddFileTask" runat="server" Text="Вставить" 
                        onclick="BAddFileTask_Click">
                    </dx:ASPxButton> 
                </td>
                <td>
                    <dx:ASPxButton ID="BDelFileTask" runat="server" Text="Удалить" 
                        onclick="BDelFileTask_Click">
                    </dx:ASPxButton> 
                </td>
            </tr>
            </table>
          <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Начальный каталог для получения списка файлов">
          </dx:ASPxLabel>
          <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="467px">
          </dx:ASPxTextBox>
                        Установить сигнатуру для сканирования:
                    <dx:ASPxTextBox ID="tbSetSignature" runat="server" Width="467px">
                    </dx:ASPxTextBox>
                        Установить расширение для отслеживания:
                    <dx:ASPxTextBox ID="tbSetExtension" runat="server" Width="467px">
                    </dx:ASPxTextBox>
                    <div>Эмуляция активности в файловой системе:</div>
                        <div style="float:left">Команда:</div>
                        <div style="float: left; padding-left: 10px;"><dx:ASPxTextBox ID="tbSetCommand" runat="server" Width="200px"></dx:ASPxTextBox></div>
                        <div style="float: left; padding-left: 10px;">Параметры:</div>
                        <div style="float: left; padding-left: 10px;"><dx:ASPxTextBox ID="tbSetCommandParams" runat="server" Width="100px"></dx:ASPxTextBox></div>
                        <div style="float: left; padding-left: 10px;">Время запуска действия:</div>
                        <div style="float: left; padding-left: 10px;"><dx:ASPxSpinEdit ID="startEmulationTime" runat="server" Number="0" Width="50px" MaxValue="9999999">
                            <ValidationSettings ErrorText="Неверное значение">
                                <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                            </ValidationSettings>
                        </dx:ASPxSpinEdit></div>
                        <div style="float:left">(в секундах)</div>
        </Content>  
    </ajaxToolkit:AccordionPane>  
    <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">  
        <Header><div id="accrg"></div><div class="acchdr">Реестр</div></Header>  
        <Content>  
        <dx:ASPxGridView ID="ASPxGridView3" runat="server" Width="100%" 
            style="margin-bottom: 5px; margin-top:5px;" AutoGenerateColumns="False" 
            KeyFieldName="f2">
            <Columns>
                <dx:GridViewDataTextColumn Caption="Задание" FieldName="f1" VisibleIndex="0" 
                    Width="300px">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Параметр" FieldName="f2" VisibleIndex="1">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsPager Visible="False">
            </SettingsPager>
            <SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
        </dx:ASPxGridView>
        <table style="width: 100%;">
            <tr>
                <td style="width: 210px">
                   <dx:ASPxComboBox ID="CBRegActiv" runat="server" ValueType="System.String" 
                        Width="200px">
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 262px">
                    <dx:ASPxTextBox ID="TBNRegTaskValue" runat="server" Width="250px" 
                        style="left:210px;" Height="20px">
                    </dx:ASPxTextBox>
                </td>
                <td style="width: 80px">
                    <dx:ASPxButton ID="BAddRegTask" runat="server" Text="Вставить" 
                        onclick="BAddRegTask_Click">
                    </dx:ASPxButton> 
                </td>
                <td>
                    <dx:ASPxButton ID="BDelRegTask" runat="server" Text="Удалить" 
                        onclick="BDelRegTask_Click">
                    </dx:ASPxButton> 
                </td>
            </tr>
            </table>
            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Ветка реестра">
            </dx:ASPxLabel>
            <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="50%" 
                AutoPostBack="True" LoadingPanelText="Загрузка&amp;hellip;">
                <Items>
                     <dx:ListEditItem Text="весь реестр" Value="full" />
                    <dx:ListEditItem Text="HKEY_CLASSES_ROOT" Value="HKEY_CLASSES_ROOT" />
                    <dx:ListEditItem Text="HKEY_CURRENT_USER" Value="HKEY_CURRENT_USER" />
                    <dx:ListEditItem Text="HKEY_LOCAL_MACHINE" Value="HKEY_LOCAL_MACHINE" />
                    <dx:ListEditItem Text="HKEY_USERS" Value="HKEY_USERS" />
                    <dx:ListEditItem Text="HKEY_CURRENT_CONFIG" Value="HKEY_CURRENT_CONFIG" />
                </Items>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
            </dx:ASPxComboBox>
            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Подветка реестра">
            </dx:ASPxLabel>
            <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="50%">
            </dx:ASPxTextBox>
        </Content>  
    </ajaxToolkit:AccordionPane>  
    <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">  
        <Header><div id="accpr"></div><div class="acchdr">Процессы</div></Header>  
        <Content>  
        <dx:ASPxGridView ID="ASPxGridView4" runat="server" Width="100%" 
            style="margin-bottom: 5px; margin-top:5px;" AutoGenerateColumns="False" 
                KeyFieldName="f2">
            <Columns>
                <dx:GridViewDataTextColumn Caption="Задание" FieldName="f1" VisibleIndex="0" 
                    Width="300px">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Параметр" FieldName="f2" VisibleIndex="1">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsPager Visible="False">
            </SettingsPager>
            <SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
        </dx:ASPxGridView>
        <table style="width: 100%;">
            <tr>
                <td style="width: 210px">
                   <dx:ASPxComboBox ID="CBProcActiv" runat="server" ValueType="System.String" 
                        Width="200px">
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 262px">
                    <dx:ASPxTextBox ID="TBProcTaskValue" runat="server" Width="250px" 
                        style="left:210px;" Height="20px">
                    </dx:ASPxTextBox>
                </td>
                <td style="width: 80px">
                    <dx:ASPxButton ID="BAddProcTask" runat="server" Text="Вставить" 
                        onclick="BAddProcTask_Click">
                    </dx:ASPxButton> 
                </td>
                <td>
                    <dx:ASPxButton ID="BDelProcTask" runat="server" Text="Удалить" 
                        onclick="BDelProcTask_Click">
                    </dx:ASPxButton> 
                </td>
            </tr>
            </table>
        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Text="Включить монитор процессов">
        </dx:ASPxCheckBox>
        </Content>  
    </ajaxToolkit:AccordionPane>  
    <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server">  
        <Header><div id="accnt"></div><div class="acchdr">Сетевая активность</div></Header>  
        <Content>  
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" 
            style="margin-bottom: 5px; margin-top:5px;" AutoGenerateColumns="False" 
            onrowdeleting="ASPxGridView1_RowDeleting" KeyFieldName="f2">
            <Columns>
                <dx:GridViewDataTextColumn Caption="Задание" FieldName="f1" VisibleIndex="0" 
                    Width="300px">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Параметр" FieldName="f2" VisibleIndex="1">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsPager Visible="False">
            </SettingsPager>
            <SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
        </dx:ASPxGridView>               
        <table style="width: 100%;">
            <tr>
                <td style="width: 210px">
                   <dx:ASPxComboBox ID="CBNetActiv" runat="server" ValueType="System.String" 
                        Width="200px" AutoPostBack="True" ViewStateMode="Enabled">
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 262px">
                    <dx:ASPxTextBox ID="TBNetTaskValue" runat="server" Width="250px" 
                        style="left:210px;" Height="20px">
                    </dx:ASPxTextBox>
                </td>
                <td style="width: 80px">
                    <dx:ASPxButton ID="BAddNetTask" runat="server" Text="Вставить" 
                        onclick="BAddNetTask_Click">
                    </dx:ASPxButton> 
                </td>
                <td>
                    <dx:ASPxButton ID="BDelNetTask" runat="server" Text="Удалить" 
                        onclick="BDelNetTask_Click">
                    </dx:ASPxButton> 
                </td>
            </tr>
            </table>
        </Content>  
    </ajaxToolkit:AccordionPane>  </Panes>
    </ajaxToolkit:Accordion>
</div>
</div>  

</asp:Content>
