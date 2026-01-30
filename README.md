# Ludu Arts - World Interaction System – Unity Developer Intern Case
**Aday:** Ece Özcan
**Pozisyon:** Unity Developer Intern  
**Süre:** ~9 saat (aktif geliştirme)  

## Unity Version
Unity 6.0.3f1 (6000.3.2f1)  

## Render Pipeline
Built-in (Core)

## Case Timebox
Start: 09:40  
Total: 12 hours

---

## Proje Özeti

Bu projede, oyuncunun oyun dünyasındaki nesnelerle etkileşime girebildiği **modüler, genişletilebilir ve okunabilir bir Interaction System** geliştirilmiştir.

Sistem; farklı interaction türlerini (Instant / Toggle / Hold), farklı interactable objeleri (Door, Key, Switch, Chest) ve bunlara bağlı **UI feedback** mekanizmalarını tek bir merkezden yönetebilecek şekilde tasarlanmıştır.

Amaç:
- Temiz mimari
- Single Responsibility
- Debug edilebilir state yönetimi
- Ludu Arts kodlama ve naming standartlarına uyum

---

## Kurulum

1. Repository’yi klonlayın
2. Unity Hub üzerinden projeyi açın
3. `Assets/[ProjectName]/Scenes/TestScene.unity` sahnesini açın
4. Play’e basın

---

## Kontroller

| Tuş | Aksiyon |
|----|--------|
| **WASD** | Hareket |
| **E** | Interact |
| **E (Basılı Tut)** | Hold interaction (Chest) |

---

## Nasıl Test Edilir

### Test Senaryosu

1. **Key Pickup**
   - Anahtar objesine yaklaş
   - `Press E to Pick Up` prompt’u görünür
   - E’ye bas → Anahtar envantere eklenir ve sahneden kaldırılır

2. **Locked Door**
   - Anahtarsızken kapıya yaklaş
   - `Key Required` mesajı gösterilir
   - Anahtarı aldıktan sonra:
     - `Press E to Open`
     - Kapı açılır / kapanır (Toggle)

3. **Switch**
   - Switch objesine yaklaş
   - E’ye bas → Bağlı kapıyı açar/kapatır
   - Kapı kilitliyse switch etkisizdir

4. **Chest (Hold Interaction)**
   - Chest’e yaklaş
   - `Hold E to Open` prompt’u görünür
   - Basılı tut → Progress bar dolar
   - Süre dolunca chest açılır ve tekrar etkileşime girilemez

---

## Mimari Kararlar

### 1. Interface Tabanlı Tasarım

**IInteractable**
- Prompt
- InteractionType
- HoldDuration
- CanInteract()
- Interact()

**IInteractor**
- Origin
- Owner

Bu yapı sayesinde:
- Oyuncu dışındaki varlıklar da ileride interactor olabilir
- Interactable objeler player’a bağımlı değildir

---

### 2. Interaction Detection

- Raycast yerine **nearest detection (Overlap + mesafe)** kullanıldı
- Aynı anda sadece **tek aktif interactable** seçilir
- InteractionDetector ayrı bir sorumluluk olarak tasarlandı

**Trade-off:**
- Raycast daha deterministik olabilir
- Ancak nearest detection çoklu objelerde daha esnek bir deneyim sundu

---

### 3. Interaction Types

| Type | Davranış |
|-----|----------|
| Instant | Tek tuş, anında |
| Toggle | Aç/Kapat |
| Hold | Süreye bağlı |

Hold interaction için:
- Timer state
- Target değişiminde reset
- Tek aktif hold target

kullanıldı.

---

### 4. UI Prompt Sistemi

**InteractionPromptView**
- Passive UI component
- Sadece görsel sorumluluk taşır
- Logic içermez

Interactor:
- Prompt text’i belirler
- Hold progress hesaplar
- UI’ya sadece veri gönderir

Bu sayede UI değiştiğinde gameplay bozulmaz.

---

### 5. Inventory Sistemi

- Minimal tutuldu
- Key odaklı
- HashSet tabanlı
- Door sadece kontrol yapar, inventory yönetmez

Bu yaklaşım coupling’i azalttı.

---

## Ludu Arts Standartlarına Uyum

Uygulanan standartlar:

- `m_` prefix (private fields)
- Region kullanımı (Fields / Unity / Methods)
- Naming convention (P_, M_, ScriptableObjects)
- Public API’lerde XML documentation
- Silent bypass yok, hatalar loglanıyor
- Prefab hierarchy kurallarına uyum

---

## Bilinen Limitasyonlar

- Inventory UI sadece basit liste (visual polish yok)
- Save/Load sistemi eklenmedi
- Animation ve sound sadece hook seviyesinde
- Highlight / outline sistemi yok

---

## Ekstra (Bonus) Özellikler

- Switch → Door zincirleme interaction
- Hold progress UI
- Dynamic prompt text
- Debug log’ları ile izlenebilir state akışı

---

## LLM Kullanımı

Bu projede LLM araçları:
- Mimari kararları tartışmak
- Bug fix sürecinde yön bulmak
- Alternatif yaklaşımları değerlendirmek

amacıyla kullanılmıştır.

Tüm etkileşimler **PROMPTS.md** dosyasında belgelenmiştir.

---

## Commit Stratejisi

- Milestone bazlı commit’ler
- Core system → Interactables → UI → Polish
- Tek commit veya dump yok

---

## Sonuç

Bu case boyunca amaç:
- “Çalışıyor”dan öte
- **okunabilir, geliştirilebilir ve savunulabilir** bir sistem kurmaktı.

Kod, mimari ve dokümantasyon bu hedef doğrultusunda hazırlanmıştır.

---

## İletişim

Her türlü soru için:
**ecemerveozcan@gmail.com**
