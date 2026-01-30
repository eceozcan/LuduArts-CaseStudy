# LLM Usage Log

## Özet
- Toplam prompt sayısı: 13
- Kullanılan araçlar: ChatGPT 5.2
- En çok yardım alınan konular:
  - Unity interaction system mimarisi
  - IInteractable / IInteractor tasarımı
  - Input handling (Instant / Toggle / Hold)
  - Interaction bug fixing ve state yönetimi
  - UI prompt & hold progress sistemi
  - Inventory, key pickup ve door locked akışı
  - Switch – Door (IToggleTarget) entegrasyonu
  - Compile-time ve API uyumsuzluğu hatalarının çözümü

---

## Prompt 1: Case Başlangıcı ve Genel Yol Haritası

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 09:40  

**Prompt:**
> Give me a step-by-step setup checklist for a 12-hour Unity technical case study with clean git history and required documentation.

**Alınan Cevap (Özet):**
> Case başlamadan önce repo yapısı, dokümantasyon dosyaları ve milestone bazlı ilerleme önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> Checklist’i birebir uygulamak yerine kendi hız ve scope’uma göre sadeleştirdim. Önce proje iskeleti ve dokümantasyonu kurup sonra geliştirmeye geçtim.

---

## Prompt 2: Interaction System için Interface Tasarımı

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 09:55  

**Prompt:**
> I need a clean interaction system in Unity.  
> How should I design IInteractable and IInteractor interfaces without overengineering?

**Alınan Cevap (Özet):**
> IInteractable için Prompt, Transform, CanInteract ve Interact metotları; IInteractor için Origin ve Owner önerildi.

**Nasıl Kullandım:**
- [x] Direkt kullandım

**Açıklama:**
> Interface’leri minimum ama genişletilebilir tuttum. Sonraki adımlarda Type ve HoldDuration eklemek bu sayede kolay oldu.

---

## Prompt 3: InteractorBehaviour ve Input (E Tuşu) Yapısı

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 10:20  

**Prompt:**
> How should an InteractorBehaviour read input and interact with IInteractable objects cleanly in Update?

**Alınan Cevap (Özet):**
> Input okuma, target güncelleme ve interaction çağrılarının net aşamalara ayrılması önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> Update akışını bilinçli şekilde parçalara böldüm (target update, input check, interact). Debug aşamasında bu yapı çok işime yaradı.

---

## Prompt 4: InteractionSettings ile Input Konfigürasyonu

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 10:35  

**Prompt:**
> I want the interact key to be configurable via a Settings asset instead of hardcoding it.

**Alınan Cevap (Özet):**
> ScriptableObject üzerinden input tanımı ve InteractorBehaviour’da settings guard kullanımı önerildi.

**Nasıl Kullandım:**
- [x] Direkt kullandım

**Açıklama:**
> E tuşunu InteractionSettings üzerinden yönettim. Daha sonra F tuşuna geçmek sorunsuz oldu.

---

## Prompt 5: Nearest Interactable Detection (Raycast vs Overlap)

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 11:05  

**Prompt:**
> Should I use raycast or overlap-based detection to find interactables?
> I want the nearest valid target in range.

**Alınan Cevap (Özet):**
> Overlap + distance comparison yöntemi, nearest selection için önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> Raycast yerine nearest detection kullandım. InteractionDetector sınıfını bu amaçla soyutladım.

---

## Prompt 6: InteractionType (Instant / Toggle / Hold) Tanımı

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 11:45  

**Prompt:**
> How should I structure Instant, Toggle and Hold interaction types in a scalable way?

**Alınan Cevap (Özet):**
> InteractionType enum kullanımı ve input handling’in switch-case ile ayrılması önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> InteractionType enum’u ekledim ve input handling’i tek merkezde topladım.

---

## Prompt 7: Compile Hataları (Type / HoldDuration)

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 13:00  

**Prompt:**
> I get compile errors:  
> 'IInteractable' does not contain Type or HoldDuration.  
> What’s the cleanest fix?

**Alınan Cevap (Özet):**
> Interface’i genişletmek veya ek interface’ler tanımlamak önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> Case süresi kısıtlı olduğu için IInteractable’ı genişletmeyi tercih ettim. Bu karar bug fix süresini ciddi azalttı.

---

## Prompt 8: Hold Interaction Bug Fix ve State Reset

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 13:20  

**Prompt:**
> My hold interaction behaves inconsistently when switching targets.
> How should hold state be managed?

**Alınan Cevap (Özet):**
> Target değiştiğinde hold timer resetlenmesi ve tek aktif hold target tutulması önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> HoldTarget, HoldTimer ve HoldTriggered state’lerini InteractorBehaviour’da yönettim.

---

## Prompt 9: UI Prompt Sistemi (Press / Hold)

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 14:00  

**Prompt:**
> How can I design a UI prompt system that shows “Press E” and “Hold E” with progress?

**Alınan Cevap (Özet):**
> UI’nın passive olması, logic’in interactor’da kalması önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> InteractionPromptView yalnızca görsel sorumluluk taşıyor. Input veya gameplay logic UI’da yok.

---

## Prompt 10: Inventory ve Key Pickup

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 15:00  

**Prompt:**
> How would you implement a simple inventory system for keys and key pickup in Unity?

**Alınan Cevap (Özet):**
> ScriptableObject key item, PlayerInventory component ve pickup interactable önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> Inventory’yi minimum tuttum. Pickup sonrası key objesini sahneden kaldırdım.

---

## Prompt 11: Locked Door Interaction

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 16:00  

**Prompt:**
> How should a locked door interaction work with inventory and toggle behavior?

**Alınan Cevap (Özet):**
> CanInteract içinde key kontrolü ve Toggle ile open/close önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> DoorInteractable inventory state’ini yönetmiyor, sadece kontrol ediyor.

---

## Prompt 12: Switch → Door (IToggleTarget)

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 17:30  

**Prompt:**
> I want a switch that toggles a door externally.
> How should IToggleTarget be used?

**Alınan Cevap (Özet):**
> Door’un IToggleTarget implement etmesi ve kilitliyken toggle’a izin vermemesi önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> Switch kapıyı kilitliyken zorla açamıyor. Mantıklı oyun davranışı sağlandı.

---

## Prompt 13: Chest Hold Interaction ve UI API Uyuşmazlığı

**Araç:** ChatGPT  
**Tarih/Saat:** 2026-01-30 18:00  

**Prompt:**
> I added a Chest with Hold interaction but get UI errors:
> InteractionPromptView has no Show/Hide methods.
> How should I fix this cleanly?

**Alınan Cevap (Özet):**
> Interactor ile UI API’lerinin uyumlu hale getirilmesi veya wrapper metot eklenmesi önerildi.

**Nasıl Kullandım:**
- [x] Adapte ettim

**Açıklama:**
> InteractorBehaviour’daki UI çağrılarını InteractionPromptView’un gerçek API’sine göre düzenledim. Chest için hold progress düzgün şekilde UI’da gösteriliyor.

---

