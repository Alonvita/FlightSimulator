<UserControl x:Class="FlightSimulator.Views.MyFlightBoard"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:FlightSimulator.Views"
             mc:Ignorable="d" 
             d:DesignHeight="450" d:DesignWidth="450">

    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width = "auto" />
            < ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height = "Auto" />
            < RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        <StackPanel HorizontalAlignment = "Center" VerticalAlignment="Center" Orientation="Horizontal" Margin="10">
            <Button x:Name="Connect"
                Content="Connect"
                Click="Connect_Click"
                ClickMode="Press"  
                Margin="0,0,10,0"
                    Height="20"
                    Width="75"
                HorizontalAlignment="Center"
                TextElement.FontWeight="Bold"
                Foreground="Gray"
                Command="{Binding ConnectCommand}"/>

            <Button x:Name="Settings"
                    Content="Settings"
                    ClickMode="Press"
                    Height="20"
                    TextElement.FontWeight="Bold"
                    Width="75"
                    HorizontalAlignment="Center"
                    Foreground="Gray"
					Command="{Binding SettingsCommand}"/>
        </StackPanel>
        <local:FlightBoard HorizontalAlignment = "Center" Grid.ColumnSpan="2" Grid.Row="1" Margin="10" x:Name="FlightBoardResource"/>
    </Grid>
</UserControl>