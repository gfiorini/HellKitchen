*** migliorie da approfondire ***

- SetSelectedCounter è onerosa come chiamata, modificare in modo che gli eventi vengano lanciati solo quando c'è un cambio effettivo di stato e non ad ogni frame
- public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged non dovrebbe essere una proprietà pubblica ma dovrebbe esserci un 
  metodo register e unregister in modo che una classe non possa rimuovere gli eventi di altre classi
- Se non si mette && this.name == "Right" va in npe   
   
   private void Update() {
          if (test && Input.GetKeyDown(KeyCode.T) && this.name == "Right"){
              Debug.Log(("T pressed!"));
              if (kitchenObject != null){
                  kitchenObject.SetCounter(secondCounter); 
                  //Debug.Log(kitchenObject.GetCounter());
              } else{
                  Debug.Log("kitchenObject is null!");
              }
          }
      }  
  
  *********************
  
  TODO:
   
   - aggiungere un tomato counter 