# C# Playwright Automation Framework — SauceDemo

A scalable web automation framework built for the SauceDemo e-commerce sandbox application. This project uses C# with Microsoft Playwright and NUnit to deliver clean, fast, and isolated integration tests without the performance overhead or driver issues of legacy Selenium setups.

---

## The Hybrid Engineering Edge
My core background is a full-stack ASP.NET developer (including a year of enterprise engineering experience at Infosys India working with C#, JavaScript, HTML5, and CSS). 

Transitioning from full-stack development to automated testing provides a major architectural advantage:
* **Stable Element Locators:** I write precise CSS, XPath, and semantic element strategies rooted in a deep understanding of DOM structure, preventing brittle test scripts.
* **Rapid Debugging:** Developer-level intuition makes it simple to read browser console loops, evaluate network tabs, and diagnose backend API payload bugs instead of just checking UI errors.

---

## Core Technical Stack
* **Language & Runtime:** C# | .NET 8.0 LTS
* **Automation Library:** Microsoft Playwright for .NET (Async/Await pipeline execution)
* **Test Runner:** NUnit 3+ Framework & Visual Studio Test Explorer Engine
* **Execution Capabilities:** Headless/Headed cross-browser testing (Chromium, WebKit, Firefox configuration ready)

---

## Implemented Test Coverage
The suite covers critical regression paths, explicit user flows, and front-end validation handling:

1. **`SuccessfulLoginTest`** — Verifies regular authentication pathways, secure session transitions, and correct dashboard redirection.
2. **`FailedLoginShowsErrorMessageTest`** — A negative test verifying that invalid passwords trigger the standard application warning banner without disrupting user flow.
3. **`BlankFieldsLoginErrorTest`** — Boundary validation test confirming the application stops empty form submissions and displays input errors natively before triggering redundant backend requests.
4. **`AddItemToShoppingCartTest`** — Automates a multi-step user interaction (adding inventory item to cart) and asserts async UI updates by verifying the cart badge increments correctly.

---

## Roadmap & Framework Adjustments
This project is built to actively expand. Next structural steps include:
* [x] Reorganizing the test files into a decoupled **Page Object Model (POM)** pattern to keep locators separated from test logic.
* [x] Implementing parameterized `[TestCase]` input matrices in NUnit to scale data-driven validation scenarios without duplicate code.
* [x] Adding an API testing layer utilizing RestSharp/Postman integration suites.
* [x] Setting up a headless regression pipeline hook via a native **GitHub Actions CI/CD workflow** to trigger tests automatically on every repository code push.
