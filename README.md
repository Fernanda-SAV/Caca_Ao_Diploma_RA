# 🎓 Caça ao Diploma — Jogo em Realidade Aumentada (AR)

## 👩‍🎓 Autoras 
**Nomes:**  
- Fernanda Sousa de Assunção Vale  
- Isabel Silva de Araújo  

**Curso/Disciplina:** Computação Gráfica  
**Instituição:** Universidade Federal do Maranhão (UFMA)  
**Ano/Semestre:** 2025.2  

---

## 📌 Apresentação do Projeto

O **Caça ao Diploma** é um jogo mobile educativo desenvolvido para **Android**, que utiliza **Realidade Aumentada (AR)** para transformar o ambiente real do usuário em um espaço interativo de aprendizado e gamificação.

No jogo, o usuário explora o ambiente real utilizando a câmera do celular. À medida que se movimenta, **elementos virtuais surgem automaticamente no espaço físico**, incentivando a exploração e a interação.

O objetivo final do jogador é **conquistar o diploma**, acumulando aprovações nas disciplinas ao longo do jogo.

Este projeto foi desenvolvido com foco em:
- Aprendizado prático de Realidade Aumentada
- Desenvolvimento de aplicações mobile
- Gamificação educacional
- Integração entre mundo real e virtual

---

## 🎮 Descrição do Jogo

### ✨ Mecânica Principal — Exploração com Brilho Misterioso
- Os objetos **não surgem por toque no chão**
- À medida que o jogador caminha, **brilhos misteriosos aparecem automaticamente** à frente da câmera
- O jogador toca no brilho para **revelar o objeto oculto**
- O brilho desaparece automaticamente caso não seja interagido
- Link do apk no Google Drive: https://drive.google.com/file/d/1KRNOcHkiOTR_fGXnFIz73CHs4zz1BCFP/view?usp=sharing
---

### 🟢 Tesouro — Aprovação
Representa a **aprovação em uma disciplina**.

- Ao ser encontrado:
  - A barra de progresso aumenta
  - Um aviso positivo aparece na tela
  - Um efeito visual de destaque é exibido

---

### 🔴 Vilões — Dificuldades Acadêmicas

#### ⚠️ Nota Baixa
- Reduz parcialmente o progresso do jogador

#### ❌ Reprovação
- Reduz significativamente o progresso

Os vilões possuem **modelos distintos**, permitindo rápida identificação visual.

---

### ⭐ Professores — Cards Premium
Os professores aparecem como **cards colecionáveis**.

- Total de **10 professores**
- Cada professor representa uma disciplina
- Não alteram o progresso do diploma
- São armazenados em um **Álbum Premium visual**
- Após coletar todos, **nenhum novo card de professor é gerado**

---

### 📘 Álbum Premium
- Interface visual dedicada
- Cards bloqueados/desbloqueados
- Contador de progresso: **x/10 professores coletados**
- Scroll vertical para navegação
- Acessível por botão durante o jogo

---

### 🏆 Objetivo Final
Ao completar **todas as disciplinas (10/10)**:
- O jogo é encerrado
- Uma **tela final** é exibida com mensagem de parabéns
- O fundo escurece completamente
- Um botão **Restart** permite reiniciar o jogo

Após o término:
- Nenhum novo objeto é gerado
- A interação com o mundo AR é encerrada

---

## 🧠 Conceitos Trabalhados

- Realidade Aumentada (AR)
- Detecção de superfícies (sem renderização visível)
- Interação por toque
- Raycasting físico
- Gamificação educacional
- Feedback visual
- Persistência de dados (PlayerPrefs)
- Gerenciamento de cenas
- UI responsiva para dispositivos móveis

---

## 🛠️ Tecnologias e Ferramentas Utilizadas

### 📱 Plataforma
- Android

### 🧩 Engine e Frameworks
- Unity 3D
- AR Foundation
- ARCore (Google)

### 💻 Linguagem
- C#

### 🎨 Interface
- Canvas UI (Screen Space Overlay)
- TextMeshPro

### 📦 Modelagem
- Modelos `.OBJ`
- Prefabs personalizados
- Ajustes de hierarquia (Root + Visual)

---

## ⚙️ Especificações Técnicas

- **Detecção de superfícies:** AR Plane Manager (planos invisíveis)
- **Spawn de objetos:** automático, baseado na posição da câmera
- **Interação:** Raycast físico
- **Sistema de entrada:** Input Manager (Old)
- **Renderização:** URP (Universal Render Pipeline)
- **Persistência:** PlayerPrefs
- **Gerenciamento de cenas:** Menu inicial + Cena AR

---

## 📁 Estrutura do Projeto

```text
Assets/
├── Scenes/
│   ├── MenuScene.unity
│   └── CacaAoDiploma.unity
├── Scripts/
│   ├── ARTapGameController.cs
│   ├── ARItem.cs
│   ├── GameManager.cs
│   ├── UIHud.cs
│   ├── AlbumUI.cs
│   ├── CollectEffect.cs
│   └── ARMysteryGlow.cs
├── Prefabs/
│   ├── Tesouro_Aprovacao.prefab
│   ├── VILAO_NotaBaixa.prefab
│   ├── VILAO_Reprovacao.prefab
│   ├── Professor_1.prefab
│   ├── ...
│   └── Professor_10.prefab
├── Materials/
├── Models/
├── Textures/
└── UI/
```

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

1. Tela inicial com logo e botão Play
2. Entrada no ambiente de Realidade Aumentada
3. Brilhos surgem automaticamente à frente do jogador
4. Toque no brilho revela: Tesouro; Vilão; Professor
5. Interface atualizada em tempo real
6. Álbum Premium acessível durante o jogo
7. Tela final exibida ao completar o objetivo

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

