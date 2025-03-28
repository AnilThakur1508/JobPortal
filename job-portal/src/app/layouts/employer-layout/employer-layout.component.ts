import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from '../../layouts/sidebar/sidebar.component';
import { NavbarComponent } from '../../layouts/navbar/navbar.component';
import { FooterComponent } from "../../shared/footer/footer.component";


@Component({
  selector: 'app-employer-layout',
  standalone: true, // ✅ Since you're using standalone components
  imports: [CommonModule, RouterModule, SidebarComponent, NavbarComponent], // ✅ Import Sidebar & Navbar
  templateUrl: './employer-layout.component.html',
  styleUrls: ['./employer-layout.component.css']
})
export class EmployerLayoutComponent { }
