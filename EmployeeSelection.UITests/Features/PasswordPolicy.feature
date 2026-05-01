Feature: Проверка надежности пароля

Scenario Outline: Проверка политики пароля
  Given пользователь вводит пароль "<password>"
  When система проверяет надежность пароля
  Then результат должен быть "<expected>"

Examples:
  | password      | expected |
  | "short"       | False    |
  | "Password"    | true     |
  | "Pass1234"    | True     |
  | "12345678"    | True     |
  | ""            | False    |