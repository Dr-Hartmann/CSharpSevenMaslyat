# CSharpSevenMaslyat
## Команда er: Семеро Маслят

- [Микуцких Григорий](https://github.com/Dr-Hartmann) - тимлид, проектировщик, сборщик; 
- [Белоус Глеб](https://github.com/Sindy101) - фротненд, интерфейс и страницы;
- [Тихомиров Владислав](https://github.com/GONEVladd20) - фронтенд, локализация (опционально) и редактирование docx-документа;
- [Харламов Денис](https://github.com/den12325) - бэкенд, контроллеры (OpenAPI), сервисы;
- [Остапенко Степан](https://github.com/Seelane) - бэкенд, контроллеры, нейросеть (опционально);
- [Маслов Владислав](https://github.com/Saifor) - БД, котекст и миграции;
- [Новиков Игорь](https://github.com/Forguebeelov) - БД, сущности/модели, логирование (опционально), сервисы.

---
## Стек
Архитектуры – MVC и MVVM.

| Фронтенд           | Бэкенд                  | БД                                    |
| :----------------- | :---------------------- | :------------------------------------ |
| Белоус, Тихомиров  | Остапенко, Харламов     | Маслов, Новиков                       |
| _Blazor_           | _RestAPI_, DTO-классы<br>      | _PostgreSQL_, _Entity Framework Core_ |
| **Blazor Web App** | OpenAPI (Swagger) | Способ работы с БД: **Code first**    |

---
## Компетенции
- **.NET** (C#)
- **ASP.NET Core MVC**
- **SOLID**
- паттерн Внедрение зависимостей (**Dependency Injection**) 
- **асинхронное программирование** и токены отмены (**Cancellation Token**)

---
## Задание до 26 апреля
Фронтенд:
- **HTML5, CSS3, Razor Pages, Blazor**
- http-запросы, валидация отправляемых и получаемых данных (**IHttpClientFactory**)
- **проект Blazor-клиент (без сервера)** с отрисовкой на клиенте и http-запросами, страницами личного кабинета, верхней панелью для инструментов, элементов навигации, имитацией http-запросов в контроллер (возьмите у Бекэнда), получением ответа (не всегда корректного 200, но и 40X, используйте Task.Delay() для имитации времени обработки), текстовыми полями ввода (при создании интерфейса по максимуму используйте компоненты Blazor/ASP.NET (зелёные), избегая созвучные с html компоненты (синие), и bootstrap (должен идти в комплекте), а интерактивность должна быть клиентской)

Бэкенд:
- **Action, Func**, Predicate
- http-запросы, валидация отправляемых и получаемых данных
- паттерн Команда (**Command**)
- паттерн Состояние (**State**)
- контроллеры, сервисы, **DI**
- **AddScoped**, **AddSingleton**, **AddControllers**, **MapOpenApi**
- валидация полученных данных перед отправкой в БД
- **проект веб-API ASP.NET Core минимальный API с помощью контроллеров**, OpenAPI и проверкой подлинности (Kestrel и CORS) с имитацией получаемых данных, их валидацией и отправкой запроса на имитированный сервис(ы) (загрузить и получить данные из одного контроллера с помощью нескольких сервисов, получить список доступных API и разделить права доступа к ним, имитировать несанкцианированный доступ)

База данных:
- **PostgreSQL**, **pgAdmin**
- **LINQ** и **SQL**
- валидация получаемых данных на уровне модели (**аннотации** для свойств)
- сервисы (****), **DI**
- **AddDbContextFactory**, **AddScoped**
- сущности, **ERD** (автогенерируемая - PlantUML)
- **проект веб-API ASP.NET Core минимальный API с помощью контроллеров** (запросы можно эмулировать по одному нажатию) с сервисами, имитацией контроллеров и получаемых данных, несколькими контекстами, где для Пользователей и Шаблонов свои разные БД (загрузить и получить данные из одного контроллера с помощью нескольких сервисов)

**Примерные сущности для БД:**
- Пользователь (Id, Никнейм, Логин, Пароль, Лист_id_Пользовательских_Документов)
- Шаблон (Id, Имя, Файл_docx)
- Пользовательский_Документ (Id, Имя, Id_Шаблона, Файл_json_заполненных_тегов_шаблона) - теги будут выглядеть примерно так: {{title}}, {{student}}, {{group}} {{teacher}}, {{year}}...

---
## Общие указания по разработке
- каждая команда работает в своём ASP.NET Core проекте (однако модули Controller и Model будут в одном проекте после _слияния_)
- перевести требуемые поля и свойства к nullable-типу
- валидация данных на каждом слое
- по выполнению задания будет проведено _слияние_ ваших проектов по типу 'Ctrl+C, Ctrl+V', где я просто соберу получившиеся модули в одно решение
- текущие MVP-проекты будут выложены для того, чтобы вы могли взять оттуда код (не факт, что проект запустится)

---
## Условия разработки
1. Основная среда разработки: **Visual Studio**.
2. Удалённая работа для всех, кроме тимлида, есть возможность задавать вопросы очно.
3. GitHub: работа в **своих** отдельных ветках, сборка некоторого результата в **development**, релизная (конечная или MVP) версия в **main**.
4. Коммиты: каждые 2 недели обязательно, спрос поимённо.

---
## Опорная идея
_**Составитель курсовой работы/отчёта по требованиям кафедры МПО ЭВМ.**_

![Макет 1](./DataBase/Img/Макет1.png)![Макет 2](./DataBase/Img/Макет2.png)
_Тип приложения: браузерное._

_Пользователь может:_
+ _зарегистрироваться как гость, пользователь или админ,_
+ _вводить содержание в текстовые блоки (менять форматирование нельзя),_
+ _создавать разделы, заголовки, пункты, подпункты и т.д.,_
+ _скачивать свой файл, сохранаять сессию в базе данных,_

_Важные моменты интерфейса:_
- _страница личного кабинета (логин, пароль, хранилище до 10 своих документов),_
- _страница выбора шаблона документа,_
- _страница редактора документа, панель инструментов_,
- _кнопка локализации на несколько языков (Русский, English, 中文, O'zbek; опционально),_
- _нейросеть (опционально)._

_Выходной формат: .docx- и .odt-файл._

_В базе данных хранятся:_
- _данные зарегистрированных пользователей (удаление неактивных?),_
- _до 5 своих документов в хранилище (не целиком файл, а сущностями в БД)._

---
## Желаемый результат
Использование приложения студентами кафедры МПО ЭВМ.

---
## Этапы разработки
1. ✔️ ~~Создание проекта в Git~~
2. ✔️ ~~Изучение своих технологий.~~
3. ✔️ ~~Разработка Mockups интерфейса.~~
4. ✔️ ~~Написание ТЗ.~~
5. ✔️ ~~Выполнить ознакомительные задания по своим технологиям.~~
6. ❌ ~~Сверстать макет интерфейса, запрограммировать ввод данных от Клиента в БД.~~ 
7. 💅 ~~Создать MVP (ориентировочно середина апреля).~~
8. Создать настоящее приложение, решающее поставленную задачу (ёжик плакал, кололся, но продолжал есть кактус).
8. Создать логотип (опционально).
9. Провести локальное тестирование по кафедре (желательно).
10. Выступление (презентация + видео или очная демонстрация).
11. Написать и сдать РПЗ (**≈**20 страниц, где около половины совпадает у всех, а остальное - индивидуально).

---
Минута молчания перед следующими проектами 🫡:
- MvpProjectV1 - старался быть похожим на настоящее приложение, но не смог (похлопаем по плечу в знак уважения)
- MvpProjectV2 и MvpProjectV3 - так и не увидели свет...
- MVPv4 - был похож на приложение, но сервер был клиентом, а клиент... он с нами в одной комнате?