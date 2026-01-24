# Market Management System

Aplikacja webowa typu **Market Management System**, stworzona w oparciu o **ASP.NET MVC** oraz zasady **Clean Architecture**.  
Projekt służy do zarządzania podstawowymi procesami sklepowymi, takimi jak produkty, kategorie oraz logika biznesowa oddzielona od warstwy UI.

---

## 🎯 Cel projektu

Celem projektu było:
- praktyczne zastosowanie wzorca **MVC**
- rozdzielenie logiki biznesowej od warstwy prezentacji
- wykorzystanie **Clean Architecture**
- praca z **Entity Framework Core**
- zbudowanie aplikacji w sposób skalowalny i testowalny

---

## 🧩 Funkcjonalności

- zarządzanie danymi domenowymi (np. produkty / encje biznesowe)
- separacja logiki biznesowej (UseCases)
- warstwowy podział projektu
- obsługa bazy danych przez Entity Framework
- aplikacja webowa oparta o ASP.NET MVC


---

## 🏗 Architektura

Projekt wykorzystuje **Clean Architecture**, dzięki czemu:

- logika biznesowa nie zależy od frameworka
- łatwo testować i rozwijać aplikację
- UI oraz baza danych są tylko szczegółami implementacyjnymi

### Struktura projektu:
```
Build-Market-Management-System
│
├── CoreBusiness
│ └── Encje domenowe i logika biznesowa
│
├── UseCases
│ └── Przypadki użycia aplikacji
│
├── Plugins
│ └── Implementacje dostępu do danych
│
├── WebApp
│ └── Warstwa MVC (Controllers, Views)
│
└── BuildMarketManagementSystem.sln
```
---

## 🛠 Technologie

- **ASP.NET MVC**
- **.NET**
- **Entity Framework Core**
- **C#**
- **Clean Architecture**
- **Dependency Injection**

---

## 🚀 Uruchomienie lokalne

1. Sklonuj repozytorium:
```bash
git clone https://github.com/TROX25/Build-Market-Management-System.git
```
2. Otwórz plik .sln w Visual Studio

3. Skonfiguruj połączenie z bazą danych (np. w appsettings.json jeśli dotyczy)

4. Przywróć pakiety NuGet

5. Uruchom projekt (F5 / dotnet run)
