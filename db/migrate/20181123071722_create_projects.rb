class CreateProjects < ActiveRecord::Migration[5.2]
  def change
    create_table :projects do |t|
      t.string :name
      t.string :mainimage
      t.text :description
      t.text :introduction
      t.string :googlplaylink
      t.string :youtubelink

      t.timestamps
    end
  end
end
