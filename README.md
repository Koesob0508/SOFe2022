# Dungeon and Signal
2022 KHU SWFestival 게임 부문 출품 작품   
   
데모영상: https://youtu.be/E9cJD2p3YWU

### 게임 개요
게임 장르: 오토 배틀, 팀 빌딩, 전략 시뮬레이션   
개발 언어: C# (Unity 2021.3.6f1)   
대상 플랫폼: Android   


<h3> <p align="center">Poster </p></h3>

<p align="center">
<img src="https://user-images.githubusercontent.com/99636089/202706444-ad42af45-a181-4e84-9f29-e99c4dc2014b.png" width = "50%" height="50%"></p>

### 게임 소개
Dungeon and Signal은 점점 강해지는 적과 싸우며 강한 아군 용병들의 조합을 맞추는 게임입니다.   
각 용병은 개별 <b>성격 유형(MBTI)</b>을 가지고 있습니다. 용병들 사이의 성격 관계에 따라 팀에 버프나 디버프가 주어집니다.    
팀 전체의 성격 관계에 따른 전체 팀 버프, 전투에 참여하는 유닛에 따른 전투 팀 버프, 같은 적을 공격하는 용병의 관계에 따라 추가 전투 효과가 발동됩니다.   
이렇게 성격 조합이 전투에 직접적으로 영향을 주기 때문에 매우 중요합니다.   
하지만 전투에 참여할 때마다 용병의 허기가 줄어들기 때문에 하나의 조합으로 계속 진행할 수 없습니다.   
전략적으로 팀을 구성하고, 전투에 참여하는 용병을 배치하여 던전을 모두 격파하세요!   



<details>
<summary><h4>게임 방법</h4></summary>
게임 시작시 원하는 용병을 선택하고, 본인 이름과 MBTI를 입력한다.   

맵에서 이동할 위치를 선택한다.   

맵은 총 3스테이지로 구성되어있으며, 점점 강한 적을 상대하여야 한다.   
<p align="center">
<h3> 해골 도시 > 죽음의 도시 > 고대 도시   </h3>
각 스테이지 보스    
<img src="https://user-images.githubusercontent.com/99636089/203457524-cfbdf4d9-cd0d-47fa-845d-8a3ad00b1155.png" width = "10%" height="10%">

<img src="https://user-images.githubusercontent.com/99636089/203457555-ce0d3f89-4f19-4ab7-a747-d78773b8a2d9.png" width = "10%" height="10%">

<img src="https://user-images.githubusercontent.com/99636089/203457580-312ac5b3-3100-4f66-bfcc-0e559d643e8a.png" width = "10%" height="10%">

 </p>

맵의 장소는 총 5가지로 구분된다.
- 숲 지역   
![image](https://user-images.githubusercontent.com/99636089/203456659-16caf1d3-7214-4a6f-80d9-f016c286fe29.png)   
   일반 적이 출현한다.   
   
- 동굴 지역   
![image](https://user-images.githubusercontent.com/99636089/203456700-9502ac6b-564f-4623-b3a3-6a9e8a4a0c7c.png)   
   강한 적이 출현한다.   

- 이벤트 지역   
![image](https://user-images.githubusercontent.com/99636089/203456677-ff966a34-8c04-483a-b188-1ab06d31e4f3.png)   
   장비 사거나 아군을 회복시키는 것과 같은 이벤트 발생한다.   
   질문 이벤트의 경우, 대답과 아군 MBTI에 따라 능력치가 변화한다.   

- 마을 지역   
![image](https://user-images.githubusercontent.com/99636089/203456785-1aab9837-5742-464b-8912-a055ea876bb0.png)   
   상점, 여관, 길드가 존재한다.    
   - 상점에서는 돈을 지불하고 장비를 살 수 있다.   
   - 여관에서는 돈을 지불하고 아군 용병의 허기를 채우거나, 체력을 회복시킬 수 있다.   
   - 길드에서는 돈을 지불하고 새로운 용병을 고요할 수 있다.   

<p align="center">
<img src="https://user-images.githubusercontent.com/99636089/203460789-83bb1d65-22ff-4ffb-a34b-4663fb8aef98.png" width = "30%" height="30%"></   
   
   <b>☆전투 중 소모되는 허기와 체력은 마을에서만 회복할 수 있음☆</b>   

- 보스 지역   
![image](https://user-images.githubusercontent.com/99636089/203456824-2a9ce976-438f-4fa0-a947-b674e0928d22.png)   
   매 스테이지 마지막에는 보스 지역이 존재하며, 해당 지역을 클리어해야만 다음 스테이지로 넘어갈 수 있다.   
</details>

<details>
<summary><h4>전투 관련</h4></summary>

전투 지역에 들어갈 경우, 전투 준비 > 전투 > 전투 결과 순으로 진행이 된다.   
1. 전투 준비 단계   
   적 유닛을 보고, 전략적으로 아군 용병을 배치해야 한다.   
   이때 배치하는 용병의 MBTI에 따라 적용되는 팀 효과가 달라진다.   
   전투에 참여하는 용병들은 10의 허기가 소모된다.   
   ![image](https://user-images.githubusercontent.com/99636089/203458320-7fa1840b-d867-4309-8e8a-9d034af467d6.png)   

   
2. 전투 단계      
   전투 시작 버튼을 누를 경우, 전투가 시작된다.   
   전투는 자동으로 진행되며, 각 캐릭터 들은 마나가 가득찰 경우 고유의 스킬을 사용한다.   
   MBTI 관계에 따라 같은 적을 공격할 경우, 추가 공격이 적용되는 것과 같은 버프가 적용된다.   
   ![image](https://user-images.githubusercontent.com/99636089/203458381-ca6ccb68-d315-46be-a78b-cb90e756c5ca.png)  
   
3. 전투 결과 단계      
   모든 적을 처치하거나 아군 용병이 전멸당하면, 전투가 종료된다.    
   모든 적을 처치할 경우, 처치한 적에 따라 돈이 주어진다.   
   모든 아군이 처치당할 경우, 해당 지역을 다시 도전해야 하며, 더 이상 싸울 용병이 없을 경우 게임이 종료된다.   
   ![image](https://user-images.githubusercontent.com/99636089/203456643-ea661857-1c33-4fe8-9141-3f64d81271da.png)   

   전투에서 잃은 체력은 유지된다.    

</details>

<details>
<summary><h4>용병 관계 관련</h4></summary> 
용병은 매 게임마다 MBTI가 정해진다.    
MBTI는 다음과 같이 3가지 요소에 적용된다.   

1. 팀 관계 랭크   

![image](https://user-images.githubusercontent.com/99636089/203456308-e3810b84-f911-4c2b-a789-e68e64e373e7.png)   

용병 사이 관계를 아래 시너지 표에 따라 팀 랭크 포인트가 정해진다.   

![image](https://user-images.githubusercontent.com/99636089/203456395-49b0de9f-a90b-4bdc-8291-2602a2ccf875.png)   

팀 랭크에 따라 전투 시 다음과 같은 추가 효과가 부여된다.   

![image](https://user-images.githubusercontent.com/99636089/203456452-c4817afa-6125-4eef-bde1-e7766b1bcc6c.png)   


2. 전투 팀 효과   
![image](https://user-images.githubusercontent.com/99636089/203456864-30efc168-ba3a-438e-bdd8-da4f167e9bad.png)   
해당 전투에 참여하는 용병에 따라 적용되는 팀 버프가 변화한다.   

3. 전투 시너지   
전투에서 관계가 좋은/나쁜 용병끼리 같은 적을 공격할 경우, 시너지 효과가 발동한다.    
![image](https://user-images.githubusercontent.com/99636089/203456578-d6de58cb-c259-4e49-9ecd-3a1008bb11b3.png)   


</details>

<details>
<summary><h4>부가적 요소</h4> </summary>

<h5> 장비 </h5>   
   
![image](https://user-images.githubusercontent.com/99636089/203458833-d789a8e2-cc31-45ad-814e-822f7e1d9b08.png)   
   
마을, 전투, 이벤트 등을 통해 장비를 구매하거나 얻을 수 있다.    
같은 등급의 같은 장비 두 개를 합쳐 다음 등급의 장비로 강화할 수 있으며, 그에 따른 효과가 증가한다.   
   
</details>

<details>
<summary><h4>기술적 요소</h4></summary>
1. [BT구현] 유니티에서 효율적인 전투 AI 구현을 위한, Behavior Tree 기능 구현.
<p align="center">
<img src="https://user-images.githubusercontent.com/99636089/202720097-54eb088f-2713-45e7-b23a-31a2e835be27.png"> </p>

2. [전투 시스템] 전투 맵의 경우 노드 단위로 구분하여, 가까운 적 탐색, 이동, 전투 시스템 구현.
<p align="center">
<img src="https://user-images.githubusercontent.com/99636089/202709877-f14688b1-0fe0-486c-978a-21342bd0b362.gif" width = "50%" height="50%"></p>

3. [절차적 맵 생성] 새 게임 시작 혹은 다음 스테이지로 넘어갈 경우, 알고리즘에 따라 절차적 지도 생성.
<img src="https://user-images.githubusercontent.com/99636089/202722466-7c79d658-bb43-4a8d-a408-7b6e9a5f9314.png">
<img src="https://user-images.githubusercontent.com/99636089/202722473-dd9c8c9c-4343-4184-ba10-50b591884dec.png">

이에 대해서는 기획에 따라 유동적으로 수정할 수 있게 시스템 구현.
![image](https://user-images.githubusercontent.com/99636089/203458964-341d9514-25a4-4cc5-9aff-5a255afb0d2a.png)   

4. 데이터 세이브&로드
현재 아군에 대한 체력, 허기, 장비와 같은 정보와 현재 클리어 스테이지 현재 맵의 형태 등을 모두 데이터로 저장함.
이어하기를 할 경우 해당 정보를 받아와 이어서 진행할 수 있음.
   
5. 로그 시스템   
전투 시 발생하는 스킬이나 적 처치와 같은 기록은 전투 중 로그로 기록됨.   
![image](https://user-images.githubusercontent.com/99636089/203457335-c5b5237e-c71e-4fef-b2e7-3dbd2df91f48.png)   


</details>

