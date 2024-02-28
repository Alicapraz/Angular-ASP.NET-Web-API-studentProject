import { Component } from '@angular/core';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import { RouterOutlet,RouterLinkActive,RouterLink } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-top-nav',
  standalone: true,
  imports: [MatToolbarModule,MatIconModule,RouterLinkActive,RouterOutlet,RouterLink,HttpClientModule],
  templateUrl: './top-nav.component.html',
  styleUrl: './top-nav.component.css'
})
export class TopNavComponent {

}
