Тестовый проект на Unity с использованием Netcode for Gameobjects (NGO)
используемые плагины:  
VContainer  
UniRX  
[Starter Assets - ThirdPerson](https://assetstore.unity.com/packages/essentials/starter-assets-thirdperson-updates-in-new-charactercontroller-pa-196526?srsltid=AfmBOoosJWzJOUYl_L2_TJSBfBAAyiZaMXuF_hmC_fQFwBMUG5tVRLfH)  

[EntryPoint](Assets/Code/Scripts/EntryPoint.cs) - входная точка в проект  
[GameStateMachine](Assets/Code/Scripts/GameStates/GameStateMachine.cs) - отвечает за переходы между состояниями стартового меню, игры, окном gameover, окном дисконекта

[PlayerService](Assets/Code/Scripts/Services/PlayerService.cs) - отвечает за работу с игроком (создает персонажа, повреждение при падении, лечение)  
[ItemsService](Assets/Code/Scripts/Services/ItemsService.cs) - генерирует объекты (аптечки)  
[StageService](Assets/Code/Scripts/Services/StageService.cs) - находит случайную точку на уровне  
[InputService](Assets/Code/Scripts/Services/InputService.cs) - слушает ввод с устройства  

[InputState](Assets/Code/Scripts/Data/InputState.cs) - текущее состояние нажатых кнопок  
[ViewsState](Assets/Code/Scripts/Data/ViewsState.cs) - текущее состояние меню

[FirstAidKit](Assets/Code/Scripts/Components/FirstAidKit.cs) - компонент с аптечкой  
[Health](Assets/Code/Scripts/Components/Health.cs) - значение здоровья синхронизирующегося по сети  
[ThirdPersonController](Assets/Code/Scripts/Components/ThirdPersonController.cs) - управление перемещением ГГ  
[NetworkItemsInjector](Assets/Code/Scripts/Components/NetworkItemsInjector.cs) - сомнительное архитектурное решение, для инъекции зависимостей в NetworkBehaviour которые были созданы через NGO

[BalanceConfig](Assets/Code/Scripts/Configs/BalanceConfig.cs) - настройки силы падения, количества лечения и прочего  
[GameAssets](Assets/Code/Scripts/Configs/GameAssets.cs) - ссылки на префабы  



https://github.com/user-attachments/assets/f79c81d7-d088-4fa3-9f71-4d0cd6732960

