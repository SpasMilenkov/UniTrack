import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaterialCardComponent } from './material-card.component';

describe('MaterialCardComponent', () => {
  let component: MaterialCardComponent;
  let fixture: ComponentFixture<MaterialCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MaterialCardComponent]
    });
    fixture = TestBed.createComponent(MaterialCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
