# Cookie Agent (C#)

A tiny demo of a C# AI agent that can call a single tool: **Bake Cookies**.  
The agent takes natural-language instructions and, when appropriate, invokes the baking tool with structured arguments, then returns the result to the user.

---

## ✨ What this shows

- Minimal **agent loop** (read → decide → act → observe → respond)  
- **Tool calling** from natural language (the “BakeCookies” tool)  

---

## 🚀 Quick start

### Prerequisites
- .NET SDK 8.0+
- An **OpenAI API key**.  
  This must be provided either:
  - in `appsettings.json` under the key `OpenAIApiKey`, **or**
  - as an environment variable named **`OpenAIApiKey`**.

### Setup
```bash
dotnet run --project AssessmentAgentFramework/AssessmentAgentFramework.csproj
```

### Example promt

- Ask the agent to bake cookies