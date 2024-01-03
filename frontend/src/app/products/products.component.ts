import {Component, EventEmitter, OnInit} from "@angular/core";
import {ProductsService, Avatar} from './products.service';
import {Router} from "@angular/router";
import {CreateAvatarComponent} from "./createAvatar.component";
import {ModalController, ToastController} from "@ionic/angular";
import {DataService} from "../data.service";
import {UpdateAvatarComponent} from "./updateAvatar.component";
import {firstValueFrom} from "rxjs";
import {environment} from "../../environments/environment";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {State} from "../../state";
import {DetailsAvatarComponent} from "./detailsAvatar.component";
import {jwtDecode} from "jwt-decode";

@Component({
  template: `
    <app-title title="AVATARS"></app-title>

    <ion-content color="light">

        <app-search (searchTextChanged)="onSearchTextEntered($event)" style="position: absolute; top: 0;"></app-search>

      <br>
      <ion-list inset="true" *ngFor="let avatar of avatar$;"> <!-- Fjernet index i -->
        <ion-item *ngIf="searchText === '' || avatar.avatar_name.toLowerCase().includes(searchText)">
          <img src="https://robohash.org/{{avatar.avatar_name}}.png" height="150px" width="150px"/>
          <ion-label>{{avatar.avatar_name}}</ion-label>
          <ion-button  (click)="details(avatar)" fill="clear">Information</ion-button> <!-- Fjernet class "button -->
          <ion-button *ngIf="this.role === 'Admin'"  (click)="updateAvatar(avatar)" fill="clear">Update</ion-button>
          <ion-label>{{avatar.avatar_price}} €</ion-label>

          <ion-button  (click)="saveData(avatar)" fill="clear" >
            <ion-icon name="cart-outline"></ion-icon>
          </ion-button>
          <ion-button *ngIf="this.role === 'Admin'"  (click)="deleteAvatar(avatar.avatar_id)" fill="clear">Delete</ion-button>
          </ion-item>
          </ion-list>
           <ion-button *ngIf="this.role === 'Admin'"  (click)="createAvatar()">Create</ion-button>
           <ion-item *ngIf="this.role === 'Admin'">


        <ion-list inset="true" *ngFor="let avatar of deletedAvatar;">
          <ion-item *ngIf="searchText === '' || avatar.avatar_name.toLowerCase().includes(searchText)"> <!-- tilføjet search i anden tabel-->
          <img src="https://robohash.org/{{avatar.avatar_name}}.png" height="150px" width="150px"/>
          <ion-label>{{avatar.avatar_name}}</ion-label>
          <ion-button  (click)="details(avatar)" fill="clear">Information</ion-button>
          <ion-label>{{avatar.avatar_price}} €</ion-label>
          <ion-button  (click)="enable(avatar.avatar_id)" fill="clear">Enable</ion-button>
          </ion-item>
          </ion-list>


      </ion-item>
    </ion-content>
  `
})

export class ProductsComponent implements OnInit {

 //avatarElement: Avatar | undefined; //Jeg har ikke fundet denne linjes relevans
  avatar$?: Avatar[];
  cartArray: Avatar[]= []; //Sådan her eller som før til at initialisering af Avatars
  deletedAvatar?: Avatar[];
  role: any;
  searchText: string = '';

  constructor(private productService: ProductsService
              , readonly router: Router
              , public modalController: ModalController
              , private data: DataService
              , public https: HttpClient
              , public state: State
              , public toastController: ToastController) {

    //   this.cartArray = [];
}



  onSearchTextEntered(searchValue: string) {
    this.searchText = searchValue.toLowerCase(); //Rettet så begge sider er lowercase
   // console.log(this.searchText)
  }

  saveData(avatar: Avatar){
    this.cartArray.push(avatar);
    sessionStorage.setItem("cart", JSON.stringify(this.cartArray))

  }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(result => {
      this.avatar$ = result.responseData;
      this.state.avatar = result.responseData;
   //  this.data.currentNumber.subscribe(avatarElement => this.avatarElement = avatarElement) //Jeg har ikke fundet relevansen af denne linje
    })

  this.productService.getAllDeletedProducts().subscribe(result =>{
    this.deletedAvatar = result.responseData;
  })
   this.getRole();

  }



  async createAvatar(){
    const modal = await this.modalController.create({ //model er rettet til modal
      component: CreateAvatarComponent
    })
    modal.present();
  }

  async updateAvatar(avatarElement: Avatar){
  this.data.changeAvatar(avatarElement)

    const modal = await this.modalController.create({
      component: UpdateAvatarComponent
    });
  modal.present();
  }

  async deleteAvatar(avatar_id: number | undefined)
  {

      await firstValueFrom(this.https.delete(environment.baseUrl + '/avatar/' + avatar_id))
      this.ngOnInit()

      const toast = await this.toastController.create({
        message: 'The avatar was successfully deleted',
        duration: 1233,
        color: "success"
        })
      toast.present();

  }

  async details(avatar: Avatar){
    this.data.changeAvatar(avatar)
    const modal = await this.modalController.create({
      component: DetailsAvatarComponent
    })
    modal.present();
  }

  public getRole(){
    const token: any = sessionStorage.getItem("token");

    const decodedToken  = jwtDecode(token);

    // @ts-ignore
   // console.log(decodedToken["IsAdmin"]!)

    // @ts-ignore
    this.role = decodedToken["IsAdmin"]!;
  }

  async enable(avatar_id: number | undefined){

      await firstValueFrom(this.https.delete(environment.baseUrl + '/avatar/enable/' + avatar_id))
      this.ngOnInit()

      const toast = await this.toastController.create({
        message: 'The avatar was successfully enabled',
        duration: 1233,
        color: "success"
      })
      toast.present();


      }


}
