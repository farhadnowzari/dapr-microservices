<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <div class="d-flex align-center">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-logo-dark.png"
          transition="scale-transition"
          width="40"
        />

        <v-img
          alt="Vuetify Name"
          class="shrink mt-1 hidden-sm-and-down"
          contain
          min-width="100"
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-name-dark.png"
          width="100"
        />
      </div>
      <v-btn :to="{ name: 'products' }" class="ml-4" text>
        <span class="mr-2">Products</span>
      </v-btn>
      <v-spacer></v-spacer>
    </v-app-bar>

    <v-main style="background-color: #f4f4f4">
      <v-snackbar
        :style="{ 'margin-bottom': calcMargin(index) }"
        :key="index"
        timeout="2000"
        time
        color="green"
        vertical
        right
        v-for="(message, index) in messages"
        v-model="message.display"
      >
        {{ message.text }}
      </v-snackbar>
      <v-btn @click="addMessage()">Add Message</v-btn>
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import { HubConnectionBuilder } from "@microsoft/signalr";

@Component
export default class App extends Vue {
  @Watch('messages', {
    deep: true
  })
  onMessagesChanged() {
    if(this.messages.find(x => !x.display))
      this.$nextTick(() => {
        this.messages.splice(0, 1);  
      });
  }

  calcMargin(index: number): string {
    return index * 60 + "px";
  }
  messages: { text: string; display: boolean }[] = [];
  addMessage(): void {
    this.messages.push({
      text: "This is a test message",
      display: true,
    });
  } 
  mounted() {
    const connection = new HubConnectionBuilder()
      .withUrl(`${process.env.VUE_APP_WEBSOCKET_BASE_URL}/notifications`)
      .withAutomaticReconnect()
      .build();

    connection.on("notification", (message) => {
      this.messages.push({
        text: message.message as string,
        display: true
      });
    });

    connection.start().then(() => {
      console.log("Websocket is connected!");
    });
  }
}
</script>
