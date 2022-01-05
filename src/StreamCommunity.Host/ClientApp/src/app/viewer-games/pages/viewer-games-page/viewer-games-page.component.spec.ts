import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewerGamesPageComponent } from './viewer-games-page.component';

describe('ViewerGamesPageComponent', () => {
  let component: ViewerGamesPageComponent;
  let fixture: ComponentFixture<ViewerGamesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewerGamesPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewerGamesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
