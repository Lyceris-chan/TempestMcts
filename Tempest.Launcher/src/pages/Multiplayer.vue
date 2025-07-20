<script setup lang="ts">
import { ref } from 'vue';
import Navigation from "@/components/ui/Navigation";
import Button from "@/components/ui/Button.vue";
import TextArea from "@/components/ui/TextArea.vue";
import Separator from "@/components/layout/Separator.vue";
import Modal from "@/components/ui/Modal.vue";

import championsData from 'src/data/champions/champions.json';

const selectedChampion = championsData[0];
const selectedTalent = ref(championsData[0].defaultTalent);
const isChampionModalVisible = ref(false);

// There's probably a billion better ways to do this it was for testing
function getChampionClassIcon(championClass: string) {
    const classIcons = {
        'Damage': '',
        'Flank': '',
        'Tank': 'https://static.wikia.nocookie.net/paladins_gamepedia/images/4/49/Class_Front_Line_Icon.png',
        'Support': 'https://static.wikia.nocookie.net/paladins_gamepedia/images/d/dc/Class_Support_Icon.png',
    };

    switch (championClass) {
        case 'Damage':
           return classIcons.Damage;
        case 'Flank':
            return classIcons.Flank;
        case 'Tank':
            return classIcons.Tank;
        case 'Support':
            return classIcons.Support;
    }
}

function showModal() {
    isChampionModalVisible.value = true;
}

function hideModal() {
    isChampionModalVisible.value = false;
}
</script>

<template>
    <Navigation.Content padding="standard">
        <!-- This modal's visuals (the styles) have been vibe coded -->
        <Modal :visible="isChampionModalVisible" title="Select Your Champion" @close="hideModal">
            <div class="champions-grid">
                <!-- Vue is awesome whattt?? anyways loop through the data and display each champion's infos and set the current champ on click -->
                <div v-for="champion in championsData" class="champion-card" @click="selectedChampion = champion; selectedTalent = selectedChampion.defaultTalent; hideModal();">
                    <img :src="champion.icon" class="champion-icon" />
                    <div class="champion-info">
                        <h3>{{ champion.name }}</h3>
                        <!-- Could probably add the class icon here -->
                        <span class="champion-class">{{ champion.class }}</span>
                    </div>
                </div>
            </div>
        </Modal>

        <div class="multiplayer-container">
            <!-- Temporary -->
            <div class="multiplayer-section">
                <TextArea maxlength="20" placeholder="Enter your username" /><br>
                <TextArea maxlength="20" placeholder="Server IP Address" /><br>
                <TextArea maxlength="20" placeholder="Password (optional)" />
                <Button style="width: 50%" @click="showModal">Select your champion</Button>
            </div>

            <Separator orientation="vertical" />

            <div class="multiplayer-section">
                <div class="champion-infos">
                    <img id="champion-banner" :src="selectedChampion.banner" />
                    <div class="role-and-name">
                        <img :src="getChampionClassIcon(selectedChampion.class)" style="width: 30px; height: 30px;">
                        <p>{{ selectedChampion.name }}</p>
                    </div>

                    <Separator orientation="horizontal" />

                    <div class="champion-talents">
                        <!-- Loop through the talents of the selected champion and display each one's icon, set the current talent on click -->
                        <div v-for="talent in selectedChampion.talents" class="talent-container">
                            <img :src="talent.icon" :class="{ selected: selectedTalent === talent.name }" class="talent-image" @click="selectedTalent = talent.name;" />
                            <!-- Check if selected talent -->
                            <div v-if="selectedTalent == talent.name" class="check-icon">✓</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </Navigation.Content>
</template>

<style scoped>
.multiplayer-container {
    display: flex;
    align-items: flex-start;
    height: 100%;
}

.multiplayer-section {
    flex: 1;
    display: flex;
    flex-direction: column;
}

#champion-banner {
    /* PLEASE KYIRO */
}

.role-and-name {
    display: flex;
    justify-content: center;
    align-items: center;
    color: var(--c-text-1);
    gap: 10px;
    margin-top: 15px;
    margin-bottom: 10px;
    font-weight: 900;
    font-size: 1.4rem;
    padding: 0 2rem;
    height: 100%;
    text-decoration: none;
    text-shadow: var(--c-black) 2px 2px 4px;
    text-transform: uppercase;
}

.champion-talents {
    display: flex;
    justify-content: space-evenly;
    align-items: center;
}

.talent-container {
    position: relative;
    cursor: pointer;
}

.talent-image {
    width: 160px;
    height: 160px;
}

.talent-image:hover {
    /* Get well cut images so that there isn't a square transparent background left, perhaps Old Viktor can do it ? */
}

.talent-image.selected {
    width: 170px;
    height: 170px;
}

/* Vibe coded the check icon */
.check-icon {
    position: absolute;
    bottom: -10px;
    right: -10px;
    background: #E2B541;
    color: #000;
    border-radius: 50%;
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
    font-weight: bold;
    font-size: 18px;
    box-shadow: 0 2px 8px rgba(255, 215, 0, 0.5);
    border: 2px solid #FFA500;
}

.champions-grid {
    display: flex;
    gap: 1rem;
    overflow-x: auto;
    padding: 1rem;
    scroll-behavior: smooth;
}

.champion-card {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 1rem;
    border: 2px solid transparent;
    border-radius: 8px;
    cursor: pointer;
    transition: all 0.2s ease;
    background: rgba(255, 255, 255, 0.05);
    min-width: 120px;
    flex-shrink: 0;
}

.champion-card:hover {
    border-color: var(--c-brand-3, #1cc6fb);
    background: rgba(28, 198, 251, 0.1);
    box-shadow: var(--c-brand-1) 0px 0px 1rem 0.4rem;
    transform: translateY(-2px);
}

.champion-icon {
    width: 64px;
    height: 64px;
    border-radius: 8px;
    margin-bottom: 0.5rem;
}

.champion-info {
    text-align: center;
}

.champion-info h3 {
    margin: 0 0 0.25rem 0;
    color: var(--c-text-1, white);
    font-size: 0.9rem;
    white-space: nowrap;
}

.champion-class {
    color: var(--c-text-2, #ccc);
    font-size: 0.8rem;
    white-space: nowrap;
}

/* AI Generated this custom scrollbar but idk if it's necessary */
.champions-grid::-webkit-scrollbar {
    height: 8px;
}

.champions-grid::-webkit-scrollbar-track {
    background: rgba(255, 255, 255, 0.1);
    border-radius: 4px;
}

.champions-grid::-webkit-scrollbar-thumb {
    background: var(--c-brand-3, #1cc6fb);
    border-radius: 4px;
}

.champions-grid::-webkit-scrollbar-thumb:hover {
    background: var(--c-brand-2, #0194d4);
}
</style>