# 🎓 Caça ao Diploma — Jogo em Realidade Aumentada (AR)

## 👩‍🎓 Autoras 
**Nome:** Fernanda Sousa de Assunção Vale e Isabel Silva de Araujo
**Curso/Disciplina:** Computação Gráfica  
**Instituição:** Universidade Federal do Maranhão
**Ano:** 2025.2  

---

## 📌 Apresentação do Projeto

O **Caça ao Diploma** é um jogo mobile educativo desenvolvido para **Android**, que utiliza **Realidade Aumentada (AR)** para transformar o ambiente real do usuário em um espaço interativo de aprendizado e gamificação.

No jogo, o usuário explora o mundo real através da câmera do celular e encontra **tesouros**, **vilões** e **personagens especiais**, representando situações comuns da vida acadêmica.  
O objetivo final é **conquistar o diploma**, acumulando aprovações nas disciplinas.

Este projeto foi desenvolvido com foco em:
- Aprendizado prático de Realidade Aumentada
- Desenvolvimento mobile
- Gamificação educacional
- Interação usuário–ambiente real

---

## 🎮 Descrição do Jogo

### 🟢 Tesouro — Aprovação
Representa a **aprovação em uma disciplina**.

- Ao ser encontrado:
  - A barra de progresso aumenta
  - Um aviso positivo aparece na tela
  - Um efeito visual de brilho é exibido

---

### 🔴 Vilões — Dificuldades Acadêmicas

#### ⚠️ Nota Baixa
- Reduz parcialmente o progresso do jogador

#### ❌ Reprovação
- Reduz mais significativamente o progresso

Os vilões possuem **diferenças visuais claras** para facilitar o entendimento do jogador.

---

### ⭐ Professores — Cards Premium
Os professores aparecem como **cards colecionáveis** especiais.

- Não alteram o progresso
- São armazenados em um **Álbum Premium**
- Incentivam a exploração contínua

---

### 🏆 Objetivo Final
Ao completar todas as disciplinas:
- A barra de progresso chega a 100%
- O jogador conquista o **Diploma**
- Uma mensagem de vitória é exibida

---

## 🧠 Conceitos Trabalhados

- Realidade Aumentada (AR)
- Interação por toque
- Detecção de planos no mundo real
- Gamificação educacional
- Feedback visual e textual
- Persistência de dados (salvamento de progresso)

---

## 🛠️ Tecnologias e Ferramentas Utilizadas

### 📱 Plataforma
- **Android**

### 🧩 Engine e Frameworks
- **Unity 3D**
- **AR Foundation**
- **ARCore (Google)**

### 💻 Linguagem
- **C#**

### 🎨 Interface
- **Canvas UI (Unity)**
- **TextMeshPro**

### 📦 Formatos de Modelo 3D
- `.OBJ`
- Prefabs do Unity + Models próprios das autoras

---

## ⚙️ Especificações Técnicas

- **Detecção de superfícies:** AR Plane Manager  
- **Interação por toque:** Raycast (Physics + AR Raycast)  
- **Sistema de UI:** Canvas em Screen Space Overlay  
- **Sistema de entrada:** Input Manager (Old)  
- **Renderização:** URP (Universal Render Pipeline)  
- **Persistência:** PlayerPrefs  

---

## 📁 Estrutura do Projeto
Assets/
├── Scenes/
│ └── CacaAoDiploma.unity
├── Scripts/
│ ├── ARTapGameController.cs
│ ├── ARItem.cs
│ ├── GameManager.cs
│ ├── UIHud.cs
│ ├── CollectEffect.cs
│ └── ARDebugStatus.cs
├── Prefabs/
│ ├── Tesouro_Aprovacao.prefab
│ ├── VILAO_NotaBaixa.prefab
│ ├── VILAO_Reprovacao.prefab
│ ├── Professor_1.prefab
│ └── Professor_2.prefab
├── Materials/
├── Models/
└── UI/


---

## ▶️ Como Executar o Projeto

### Pré-requisitos
- Unity instalado (versão com suporte a AR Foundation)
- Android Studio (para drivers e SDK)
- Celular Android compatível com **ARCore**
- Google Play Services for AR instalado no dispositivo

### Passos
1. Abrir o projeto no Unity
2. Conectar o celular Android via USB
3. Ativar **Depuração USB**
4. Selecionar **Build & Run**
5. Conceder permissão de câmera ao aplicativo

---

## 🧪 Funcionamento Esperado

1. A câmera do celular é aberta
2. Superfícies reais são detectadas (planos brancos)
3. Ao tocar no chão:
   - Um item é gerado
4. Ao tocar no item:
   - Tesouro → progresso aumenta
   - Vilão → progresso diminui
   - Professor → card coletado
5. A UI é atualizada em tempo real

---

## 📈 Possíveis Melhorias Futuras

- Sistema de mapa com localização GPS
- Sons e trilha sonora
- Animações mais avançadas
- Álbum Premium visual completo
- Ranking de jogadores
- Integração com banco de dados online
- Versão iOS (ARKit)

---

## 📚 Considerações Finais

O **Caça ao Diploma** demonstra como a Realidade Aumentada pode ser utilizada como ferramenta educacional, promovendo engajamento, aprendizado ativo e exploração do ambiente real de forma lúdica.

O projeto foi desenvolvido com foco em clareza, modularidade e facilidade de expansão futura.

---

🎓 *Projeto desenvolvido para fins acadêmicos e educacionais.*

