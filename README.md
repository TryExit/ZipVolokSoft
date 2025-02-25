# **ZipVolokSoft**

### **Мощный архиватор для РЕД ОС**

**ZipVolokSoft** — это современный инструмент для работы с архивами, разработанный специально для операционной системы РЕД ОС.
Программа предлагает простоту использования, высокую производительность и поддержку популярных форматов архивов.

---

# **Инструкция: работа с ZipVolokSoft**

### **Шаг 1. Раздвоение репозитория на GitHub**

1. Откройте [GitHub-страницу проекта ZipVolokSoft](https://github.com/TryExit/ZipVolokSoft).
2. Нажмите на кнопку **Fork** в правом верхнем углу страницы.
3. В появившемся окне выберите свой профиль. Теперь проект появится в вашем списке репозиториев.

### **Шаг 2. Скачивание проекта с помощью GitHub Desktop**

1. Перейдите в ваш раздвоенный репозиторий.
2. Нажмите на кнопку **Code** (зеленая кнопка).
3. В выпадающем меню выберите **Open with GitHub Desktop**.
4. Если у вас еще не установлен GitHub Desktop:
   - Скачайте и установите его с официального сайта: [desktop.github.com](https://desktop.github.com).
   - После установки повторите этот шаг.
5. В открывшемся окне GitHub Desktop выберите папку для загрузки репозитория и нажмите **Clone**.
6. Можно скачать ZIP-файл, и открыть проект через него.

### **Шаг 3. Скачивание проекта с помощью GitHub Desktop**

1. Откройте ZIP-файл, и вытащите оттуда папку с проектом. Внутри лежит программа, файл README, Excel-таблица, и презентация PowerPoint.
   Есть другой вариант через GitHub Desktop. Если вы вошли через него, то октройте папку репозитория, и увидите тоже самое.
2. Откройте файл ZipVolokSoft.sln через Visual Studio 2022
3. После загрузки проекта нажмите правой кнопкой по решению и нажмите "Опубликовать".
4. Опубликованный файл лежит на Вашем Рабочем столе, или в любой другой папке.

### **Шаг 4. Открытие на РЕД ОС**

1. Готовое приложение надо открыть на системе РЕД ОС. Для этого откройте систему, и установите WINE.
2. После установки WINE перекиньте приложение на РЕД ОС.
3. Открйоте приложение.

Приложение прекрасно работает и без системы РЕД ОС, но существует конвертация под неё.

---

## **Ход создания проекта**

### **1. Организация репозитория**

Проект начал с создания структуры репозитория, адаптированной под РЕД ОС.

### **2. Формирование команды разработчиков**

В команду вошли:

- **ChaiTryExit** и **PepperMintVandelay** _(один участник, но два разных аккаунта для работы)_.
- **S4ne4k4** _(второй участник)_.

### **3. Разделение работы по веткам**

Для упрощения разработки созданы ветки репозитория:

- **Chai** — представительская часть.
- **Mint** — проектирование архитектуры.
- **Alex** — код и тестирование.

### **4. Введение инструментов планирования**

Создан Excel-файл для отслеживания задач и их выполнения, включающий:

- Задачи команды.
- Статус выполнения на каждом этапе.

### **5. Разработка интерфейса**

Прототип интерфейса включает:

- **Кнопки управления архивами** (создание, извлечение).
- **Список файлов** с возможностью добавления и удаления.
- **Настройки** форматов и уровня сжатия.

### **6. Первый тест на РЕД ОС**

При тестировании выявлены несоответствия между кодом приложения и требованиями системы. Команда приступила к пересборке проекта с учетом выявленных проблем.

### **7. Пересбор проекта**

Проект за ночь был улучшен и доделаны все базовые функции, требуемые для обычного пользователя. Можно производить выгрузку на РЕД ОС.

### **8. Портирование на РЕД ОС**

Использована система WINE для связи протоколов .NET и системного кода Linux.

---

## **Следите за обновлениями!**

Разработка ZipVolokSoft продолжается, и мы готовим для вас еще больше улучшений.
