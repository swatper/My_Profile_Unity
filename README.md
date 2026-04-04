# 🎮 My_Profile_Unity
> **개발자 포트폴리오를 담은 'Debug Village' & 뱀서라이크 'Dev Survival'**

본 프로젝트는 단순한 게임 구현을 넘어, 유지보수가 용이한 객체지향 설계(OOP)와 웹 환경에서의 성능 최적화를 목표로 한 개인 프로젝트입니다.


## 🚀 프로젝트 구성

### 🏡 1. Debug Village (Portfolio Hub)
  - 소개: 개발자의 포트폴리오를 마을 형태의 인터렉티브 요소로 구현한 웹 버전 게임입니다.
 **핵심 기능**:
  - **Observer Pattern**: `TimeSensitiveController`를 통해 시간대(낮/밤)에 따른 월드 환경(조명, 사운드 등)을 동적으로 변경합니다.
  - **상속 및 추상화**: `BuildingBase` 추상 클래스를 활용해 다양한 상호작용 건물(School, Smithy 등)의 재사용성을 높였습니다.
  - **Skin System**: 마네킹 상호작용을 통해 캐릭터 스프라이트를 실시간으로 변경하는 시스템을 구축했습니다.

### ⚔️ 2. Dev Survival (Vampire Survivors-like)
- **소개**: 데이터 중심 설계와 최적화 기법을 적용한 서바이벌 액션 게임입니다.
- **핵심 기능**:
    - **Generic SO System**: `ScriptableObject`와 **제네릭**을 결합하여 무기, 몬스터, 경험치 데이터를 코드 수정 없이 확장 가능하도록 설계했습니다.
    - **Interface Driven Upgrade**: `IUpgradable` 인터페이스를 통해 다양한 게임 내 요소의 강화 로직을 통일했습니다.
    - **Object Pooling**: 큐(Queue) 기반의 커스텀 풀링 시스템을 직접 구현하여 대량의 투사체 생성 시의 성능 부하를 방지했습니다.

## 🛠 기술적 도전 및 최적화 (Optimization)

### ⚡ WebGL 프레임 드랍 해결 (30FPS → 60FPS)
- **문제**: 웹 페이지의 배경 CSS 애니메이션(DOM)과 Unity WebGL 엔진 간의 리소스 경합으로 인해 게임 프레임이 30FPS로 저하되는 현상이 발생했습니다.
- **해결**: 
    - **JS-Unity 연동**: `mouseenter` 이벤트를 감지하여 게임 화면 진입 시 외부 CSS 애니메이션을 `paused` 상태로 전환, 가용 리소스를 엔진에 집중시켰습니다.
    - **메모리 최적화**: 프로젝트의 Initial/Maximum Memory Size를 1024MB로 조정하고 프레임 제한을 해제했습니다.

### 🖼️ Draw Call 최적화
- **문제**: 개별 이미지 사용으로 인한 GPU 부하(Draw Call)가 증가했습니다.
- **해결**: 유사한 용도의 이미지들을 **Sprite Atlas** 형태(Multiple Sprite Mode)로 합쳐 관리함으로써 렌더링 효율을 높였습니다.

### 🌐 GitHub Pages 배포 이슈
- **문제**: GitHub Pages 서버가 Unity의 Brotli/Gzip 압축 파일을 자동으로 해제하지 못해 게임 실행이 불가능했습니다.
- **해결**: 빌드 설정에서 `Compression Format`을 `Disable`로 변경하여 호환성을 확보했습니다.

## 🏗 System Architecture

### [Scriptable Object Inheritance]
- `BaseUpgradeData<T>` (Abstract)
    - `WeaponData` (struct: `WeaponStat`)
    - `MonsterData` (struct: `MonsterStat`)
    - `ExpData` (struct: `Exp`)

### [Upgrade System Logic]
- `IUpgradable` (Interface) ⮕ `BaseUpgradeModel<T>` (MonoBehaviour) ⮕ `BaseWeapon` (Abstract)

## 🔗 관련 링크
  - Demo URL: [실시간 데모 플레이](https://swatper.github.io/)
 
