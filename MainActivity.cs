using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content;
using Android.OS;                   
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace ArabTradersGroup_v1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener 
    {

        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        RecyclerViewAdapter mAdapter;
        PhotoAlbum mPhotoAlbum;
        Fragments.SearchFragment searchFragment;
        bool searchfragmentstatus;
        //Stack<Android.Support.V4.App.Fragment> stackFragment;

        Android.Support.V4.App.FragmentTransaction trans;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            // Instantiate the photo album:
            mPhotoAlbum = new PhotoAlbum();

            trans = SupportFragmentManager.BeginTransaction();


            // Get our RecyclerView layout:
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            //............................................................
            // Layout Manager Setup:

            // Use the built-in linear layout manager:
            mLayoutManager = new LinearLayoutManager(this);

            // Or use the built-in grid layout manager (two horizontal rows):
            // mLayoutManager = new GridLayoutManager
            //        (this, 2, GridLayoutManager.Horizontal, false);

            // Plug the layout manager into the RecyclerView:
            mRecyclerView.SetLayoutManager(mLayoutManager);

            //............................................................
            // Adapter Setup:

            // Create an adapter for the RecyclerView, and pass it the
            // data set (the photo album) to manage:
            mAdapter = new RecyclerViewAdapter(mPhotoAlbum);

            // Register the item click handler (below) with the adapter:
            mAdapter.ItemClick += OnItemClick;

            // Plug the adapter into the RecyclerView:
            mRecyclerView.SetAdapter(mAdapter);

            //............................................................
            searchFragment = new Fragments.SearchFragment();
            trans.Add(Resource.Id.fragmentcontainer, searchFragment, "SearchFragment");
            trans.Hide(searchFragment);
            trans.Commit();

            searchfragmentstatus = false;
            //stackFragment = new Stack<Android.Support.V4.App.Fragment>();
            navigationView.Menu.GetItem(0).SetChecked(true);

        }

        // Handler for the item click event:
        void OnItemClick(object sender, int position)
        {
            // Display a toast that briefly shows the enumeration of the selected photo:
            int photoNum = position + 1;
            Toast.MakeText(this, "This is photo number " + photoNum, ToastLength.Short).Show();
        }

        public override void OnBackPressed()
        {
            if (SupportFragmentManager.BackStackEntryCount > 0)
            {

                SupportFragmentManager.PopBackStack();
            }
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }

          
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

           


            return  base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_search)
            {
                searchfragmentstatus = !searchfragmentstatus;
              
                if (searchfragmentstatus)
                {

                    ShowSearchFragment(searchFragment);
                 
                    
                   
                   
                }
                else
                {

                    HideSearchFragment(searchFragment);
                }

                


            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                // Handle the camera action
                Intent hIntent = new Intent(this,typeof(MainActivity));
                StartActivity(hIntent);
            }
            else if (id == Resource.Id.nav_gallery)
            {
                Intent pIntent = new Intent(this, typeof(ProductsActivity));
                StartActivity(pIntent);
            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public void ShowSearchFragment(Android.Support.V4.App.Fragment fragment)
        {

            var trans = SupportFragmentManager.BeginTransaction();
            trans.SetCustomAnimations(Android.Resource.Animator.FadeIn, Android.Resource.Animator.FadeOut);
            trans.Show(fragment);
            trans.Commit();


        }
        public void HideSearchFragment(Android.Support.V4.App.Fragment fragment)
        {

            var trans = SupportFragmentManager.BeginTransaction();
            trans.SetCustomAnimations(Android.Resource.Animator.FadeIn, Android.Resource.Animator.FadeOut);
            trans.Hide(fragment);
            trans.Commit();


        }

    }
}

