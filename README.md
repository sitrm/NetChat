# Сетевой чат

Этот проект представляет собой сетевой чат, состоящий из серверной и клиентской частей. Серверная часть (ChatHost) управляет соединениями и обменом сообщениями между клиентами, а клиентская часть (ChatClient) позволяет пользователям отправлять и получать сообщения.

## Установка и запуск

### 1. Запуск сервера

Для запуска сервера выполните следующие шаги:

1. **Сборка проекта ChatHost**:
   - Откройте проект `ChatHost` в вашей среде разработки (например, Visual Studio).
   - Соберите проект

2. **Запуск сервера**:
   - Перейдите в папку с выходными данными проекта (обычно это `bin/Debug` или `bin/Release`).
   - Найдите файл `ChatHost.exe`.
   - Запустите `ChatHost.exe` от имени администратора. 

3. **Настройка TCP порта**:
   - Откройте файл конфигурации `App.config`, который находится в папке проекта `ChatHost`.
   - Найдите раздел, отвечающий за настройки TCP, и укажите правильный адрес и номер порта для поднятия сервера.
   - Пример конфигурации:
   ```xml
   
   <services>
	   <service name="wcfChat.ServiceChat" behaviorConfiguration="mexBeh">
		   <endpoint address="" binding="netTcpBinding" contract="wcfChat.IServiceChat"/>
		      <endpoint address="mex" binding="mexHttpBinding" contract="IMetadataExchange" />
		        <host>
			         <baseAddresses>
				        <add baseAddress="http://localhost:8301" />
				        <add baseAddress="net.tcp://localhost:8302" />
			         </baseAddresses>
		       </host>
	      </service>
    </services> 
   
    ```
     

### 2. Настройка клиентской части

Для подключения клиента к серверу выполните следующие шаги:

1. **Настройка TCP порта**:
   - Откройте файл конфигурации `App.config`, который находится в папке проекта `ChatClient`.
   - Убедитесь, что адрес и номер порта совпадают с теми, что указаны в конфигурации серверной части.
   - Пример конфигурации:
   ```xml
   <client>
     <endpoint address="net.tcp://localhost:8302/" binding="netTcpBinding"
        bindingConfiguration="NetTcpBinding_IServiceChat" contract="ServiceChat.IServiceChat"
        name="NetTcpBinding_IServiceChat">
        <identity>
            <userPrincipalName value="DESKTOP-GD0O982\master" />
        </identity>
     </endpoint>
   </client>
   ```

2. **Запуск клиента**:
   - Соберите проект `ChatClient` и найдите скомпилированный файл `ChatClient.exe`.
   - Запустите `ChatClient.exe`, чтобы подключиться к серверу.

## Использование

После успешного запуска сервера и клиента вы сможете начать общение. Клиенты могут отправлять сообщения, которые будут передаваться через сервер другим подключенным клиентам.

### Примечания

- Убедитесь, что ваш брандмауэр или антивирус не блокирует указанный TCP порт.
- Для корректной работы приложения рекомендуется использовать последнюю версию .NET Framework.

