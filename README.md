# Сервис сериализализации XLS в JSON

Необходимо разработать микросервис сериализующий **XLS** в **JSON** формат. 

## Общие требования

Сервис должен быть: 
- контейнеризирован в Docker;
- stateless
- standalone

## Требования к стеку технологий
- C# .NET Core 3.1 или выше

## Требования к API
Сервис должен реализовывать REST HTTP API (с документацией в Swagger)

| Endpoint        | Метод | Описание   |
|-----------------|-------|-------------------------------------|
|`http://{сервис}/serielizer`| POST|  **URL parameters**: <br> - нет <br> **Request body**:<br>- Content: файл в формате MS Excel (\*.xls/\*.xlsx)<br>- Type: `multipart/form-data`<br> **Responses**: <br>- Code: 200<br>- Type: `application/json`<br>- Content: JSON с сериализованными данными из файла Excel|
|`http://{сервис}/swagger`   | GET | Страница с документацией API (сгенерированая swagger) |
